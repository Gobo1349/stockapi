using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using stockapi.Configuration.MW;

namespace stockapi.Infrastructure;
// подключаем MW, сваггер и т д 
public static class HostBuilderExtensions // расширения для билдера хоста 
{
    public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // регистрируем тут сервисы, они будут использоваться при обработке запроса
            services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
            services.AddSingleton<IStartupFilter, RequestLoggingMWMWFilter>();
            services.AddSingleton<IStartupFilter, VersionMWFilter>();
            services.AddSingleton<IStartupFilter, LiveMWFilter>();
            services.AddSingleton<IStartupFilter, ReadyMWFilter>();

            services.AddControllers(options =>
                options.Filters.Add<GlobalExceptionFilter>()); // подключаем фильтр - обработку исключений 
            services.AddGrpc(); // инфраструктура для Grpc 

            // генератор для сваггера 
            services.AddSwaggerGen(options =>
            {
                // инфа для сваггера - название и т д 
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StockApi", Version = "v1" });
            });
        }
        );
        return builder;
    }
}

// IStartupFilter - позволяет выстраивать пайплайн из методов Configure - спрячем туда регистрацию MW 
public class ReadyMWFilter : IStartupFilter // позвоняет загружать часть кода из Configure отдельным классом 
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.Map("/ready", builder => builder.UseMiddleware<ReadyMW>());
            next(app);
        };
    }
}

public class VersionMWFilter : IStartupFilter // позвоняет загружать часть кода из Configure отдельным классом 
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            // на URL "version" нужно повесить наш MW 
            app.Map("/version", builder => builder.UseMiddleware<VersionMW>());
            next(app);
        };
    }
}

public class LiveMWFilter : IStartupFilter // позвоняет загружать часть кода из Configure отдельным классом 
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.Map("/live", builder => builder.UseMiddleware<LiveMW>());
            next(app);
        };
    }
}

// напишем Startup фильтр, куда поместим конфигурацию сваггера 
public class SwaggerStartupFilter : IStartupFilter // позвоняет загружать часть кода из Configure отдельным классом 
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            // MW компоненты для сваггера 
            SwaggerBuilderExtensions
                .UseSwagger(app); // терминальный MW компонент - будет отдавать документ (openAPI) по определенному URL 
            app.UseSwaggerUI(); // для интерфейса - потыкать - тоже терминальный 
            next(app); // здесь будет выполняться след SF - SF устроены как матрешка - один в другом, самый маленький - Configure 
        };
    }
}

public class RequestLoggingMWMWFilter : IStartupFilter // позвоняет загружать часть кода из Configure отдельным классом 
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UseMiddleware<RequestLoggingMW>();
            next(app);
        };
    }
}

// GEF - обработка исключений, он один на приложение 
public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    // метод вызывается при исключении 
    public override void OnException(ExceptionContext context) // изначально метод пустой, поэтому переопределяем 
    {
        base.OnException(context);
        var resultObject = new // анонимный тип 
        {
            ExceptionType = context.Exception.GetType().FullName,
            Message = context.Exception.Message
        };

        // объект результата 
        var jsonResult = new JsonResult(resultObject)
        {
            StatusCode = StatusCodes.Status500InternalServerError // ошибка 500 
        };
        context.Result = jsonResult;
        var jsonResult_string = context.Result.ToString();
    }
}
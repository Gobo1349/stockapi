using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using stockapi.Configuration.MW;
using stockapi.GrpcServices;
using stockapi.Infrastructure;
using stockApi.Infrastructure.Extensions;
using stockapi.Services;
// last version 
namespace stockapi
{
    public class Startup // конфигурирует хост - он содержит элементы для обработки запроса
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) // конфигурирует DI контейнер
        {  
            services.AddSingleton<IStockService, StockService>();
           services.AddInfrastructure(); // для медиатора 
        }

        public void Configure(IApplicationBuilder app) // регистрируем(?) само приложение - пайплайн обработки запроса 
        {
            // app.Map("/live", builder => builder.UseMiddleware<LiveMW>());

            // app.UseMiddleware<RequestLoggingMW>();
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // эндпоинты определяют то, с чем будет дальше работать приложение 

                // указываем, какой сервис отвечает за обработку Grpc запроса 
                endpoints.MapGrpcService<StockApiGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace stockapi.Configuration.MW;

// MW - логирование запросов 
public class RequestLoggingMW
{
    private readonly RequestDelegate _next; // чтобы логировать - нужен логгер 
    // логгинг по умолч подключен в ASP.net Core 
    private readonly ILogger<RequestLoggingMW> _logger; 
    public RequestLoggingMW(RequestDelegate next, ILogger<RequestLoggingMW> logger) // на вход - след элемент в цепочке выполнения 
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequest(context);
        await _next(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        // приходит запрос - можем получить инфу о том, что пришло в реквесте 
        if (context.Request.ContentLength > 0) // если в запросе что то есть - давайте логировать
        {
            try // если не выполним запрос из-за логирования - плохо 
            {
                context.Request.EnableBuffering(); // чтобы прочитать body несколько раз 

                var buffer = new byte[context.Request.ContentLength.Value];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length); // считываем тело запроса в буфер 
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                _logger.LogInformation("Request logged");
                _logger.LogInformation(bodyAsText);

                context.Request.Body.Position = 0; // прочитали - body обратно 
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }
    } 
}
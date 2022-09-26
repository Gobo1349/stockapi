using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace stockapi.Configuration.MW;

// будет отдавать нам версию 
public class VersionMW
{
    public VersionMW(RequestDelegate next) // на вход - след элемент в цепочке выполнения 
    {
        
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
        await context.Response.WriteAsync(version.ToString());
    }
}
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace stockapi.Configuration.MW;

// будет отдавать нам версию - доп MW созданный по заданию, конкрентной логики нет  
public class LiveMW
{
    public LiveMW(RequestDelegate next) // на вход - след элемент в цепочке выполнения 
    {
        
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //    return new HttpStatusCodeResult(HttpStatusCode.OK);  // OK = 200
        // context.Response.StatusCode = StatusCodes.Status200OK;
        var live = context.Response.StatusCode;
        //context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsync(live.ToString());
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using stockapi;
using stockapi.Infrastructure;

CreateHostBuilder(args).Build().Run(); // создает хост - все компоненты, в т ч сервер, и конфигурации 
// new branch!
// метод main генерируется сам 

// задает точку входа в приложение
static IHostBuilder CreateHostBuilder(string[] args)
    => Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
            webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }
        ).AddInfrastructure(); // добавляем инфраструктуру (MW) на уровне Program 
    
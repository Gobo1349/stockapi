using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            // регистрируем тут сервисы, они будут использоваться при обработке запроса
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app) // регистрируем(?) само приложение - пайплайн обработки запроса 
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {   // эндпоинты определяют то, с чем будет дальше работать приложение 
                   endpoints.MapControllers();
            });
        }
    }
}
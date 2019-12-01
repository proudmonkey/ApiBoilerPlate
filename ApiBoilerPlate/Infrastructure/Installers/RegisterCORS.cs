using ApiBoilerPlate.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    internal class RegisterCors : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Configure CORS to allow any origin, header and method. 
            //Change the CORS policy based on your requirements.
            //More info see: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.0
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}

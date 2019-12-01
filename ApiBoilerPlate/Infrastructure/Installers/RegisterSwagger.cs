using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    internal class RegisterSwagger: IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Swagger
            //See: https://www.scottbrady91.com/Identity-Server/ASPNET-Core-Swagger-UI-Authorization-using-IdentityServer4
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiBoilerPlate ASP.NET Core API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    Description = "Enter 'Bearer' following by space and JWT.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,

                });

                options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
            });
        }
    }
}

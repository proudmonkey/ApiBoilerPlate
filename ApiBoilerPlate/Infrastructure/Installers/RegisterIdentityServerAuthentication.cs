using ApiBoilerPlate.Contracts;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    internal class RegisterIdentityServerAuthentication: IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Setup JWT Authentication Handler with IdentityServer4
            //You should register the ApiName a.k.a Audience in your AuthServer
            //More info see: http://docs.identityserver.io/en/latest/topics/apis.html
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = config["ApiResourceBaseUrls:AuthServer"];
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "api.boilerplate.core";
                    });
        }
    }
}

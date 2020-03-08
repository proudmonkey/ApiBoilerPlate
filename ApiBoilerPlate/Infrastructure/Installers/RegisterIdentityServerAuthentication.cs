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
            //For an example on how to build a simple Token server using IdentityServer4,
            //See: http://vmsdurano.com/apiboilerplate-and-identityserver4-access-control-for-apis/

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

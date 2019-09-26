using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Installer
{
    public class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IPersonManager, PersonManager>();
        }
    }
}

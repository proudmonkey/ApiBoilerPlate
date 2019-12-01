using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.DataManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Interface Mappings for Repositories
            services.AddTransient<IPersonManager, PersonManager>();
        }
    }
}

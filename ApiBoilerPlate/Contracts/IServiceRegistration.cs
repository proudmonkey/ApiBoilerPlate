using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}

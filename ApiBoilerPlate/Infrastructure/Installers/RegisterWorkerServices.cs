using ApiBoilerPlate.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    public class RegisterWorkerServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Uncomment to Register Worker Service
            //services.AddSingleton<IHostedService, MessageQueueProcessorService>();
        }
    }
}

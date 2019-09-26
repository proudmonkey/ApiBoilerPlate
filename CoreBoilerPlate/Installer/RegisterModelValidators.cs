using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.DTO;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiBoilerPlate.Installer
{
    public class RegisterModelValidators: IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<PersonDTO>, PersonDTOValidator>();
        }
    }
}

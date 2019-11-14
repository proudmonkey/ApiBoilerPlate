using ApiBoilerPlate.Constants;
using ApiBoilerPlate.Contracts;
using AutoWrapper.Wrappers;
using ApiBoilerPlate.DTO;
using ApiBoilerPlate.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Installers
{
    public class RegisterApiResources : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {

            services.AddHttpClient<IApiConnect<ApiResponse> ,SampleApiConnect >(client => {
                client.BaseAddress = new Uri(config["ApiResourceBaseUrl:SampleApi"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentMediaTypes.JSON));
            });

            services.AddHttpClient<IAuthServerConnect, AuthServerConnect>();

        }
    }
}

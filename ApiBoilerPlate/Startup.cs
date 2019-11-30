using ApiBoilerPlate.Infrastructure.Configs;
using ApiBoilerPlate.Infrastructure.Extensions;
using ApiBoilerPlate.Infrastructure.Filters;
using AspNetCoreRateLimit;
using AutoMapper;
using AutoWrapper;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiBoilerPlate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Uncomment to Register Worker Service
            //services.AddSingleton<IHostedService, MessageQueueProcessorService>();

            //Register services in Installers folder
            services.AddServicesInAssembly(Configuration);




            //Register MVC/Web API, NewtonsoftJson and add FluentValidation Support
            services.AddControllers()
                    .AddNewtonsoftJson()
                    .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });

            //Setup JWT Authentication Handler with IdentityServer4
            //You should register the ApiName a.k.a Audience in your AuthServer
            //More info see: http://docs.identityserver.io/en/latest/topics/apis.html
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                       options.Authority = Configuration["ApiResourceBaseUrls:AuthServer"];
                       options.RequireHttpsMetadata = false;
                       options.ApiName = "api.boilerplate.core";
                    });

            //Register Automapper
            services.AddAutoMapper(typeof(MappingProfileConfiguration));

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Enable CORS
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            //Enable Swagger and SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiBoilerPlate ASP.NET Core API v1");
            });

            //Enable AspNetCoreRateLimit
            app.UseIpRateLimiting();

            //Enable HealthChecks and UI
            app.UseHealthChecks("/selfcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).UseHealthChecksUI();

            ////Enable HealthChecks and UI
            //app.UseHealthChecksUI();

            //Enable AutoWrapper.Core
            //More info see: https://github.com/proudmonkey/AutoWrapper
            app.UseApiResponseAndExceptionWrapper();

            app.UseRouting();

            //Adds authenticaton middleware to the pipeline so authentication will be performed automatically on each request to host
            app.UseAuthentication();

            //Adds authorization middleware to the pipeline to make sure the Api endpoint cannot be accessed by anonymous clients
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

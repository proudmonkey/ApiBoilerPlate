using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Infrastructure.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace ApiBoilerPlate.Infrastructure.Installers
{
    internal class RegisterHealthChecks : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register HealthChecks and UI
            services.AddHealthChecks()
                    .AddCheck("Google Ping", new PingHealthCheck("www.google.com", 100))
                    .AddCheck("Bing Ping", new PingHealthCheck("www.bing.com", 100))
                    .AddUrlGroup(new Uri(config["ApiResourceBaseUrls:AuthServer"]),
                                name: "Auth Server",
                                failureStatus: HealthStatus.Degraded)
                    .AddUrlGroup(new Uri(config["ApiResourceBaseUrls:SampleApi"]),
                                name: "External Api",
                                failureStatus: HealthStatus.Degraded)
                    .AddNpgSql(config["ConnectionStrings:PostgreSQLConnectionString"],
                                name: "PostgreSQL",
                                failureStatus: HealthStatus.Unhealthy)
                    .AddSqlServer(
                                connectionString: config["ConnectionStrings:SQLDBConnectionString"],
                                healthQuery: "SELECT 1;",
                                name: "SQL",
                                failureStatus: HealthStatus.Degraded,
                                tags: new string[] { "db", "sql", "sqlserver" });

            services.AddHealthChecksUI();
        }
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Infrastructure.HealthChecks
{
    internal class PingHealthCheck : IHealthCheck
    {
        private string _host;
        private int _timeout;

        public PingHealthCheck(string host, int timeout)
        {
            _host = host;
            _timeout = timeout;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(_host, _timeout);
                    if (reply.Status != IPStatus.Success)
                    {
                        return HealthCheckResult.Unhealthy($"Ping check status [{ reply.Status }]. Host {_host} did not respond within {_timeout} ms.");
                    }

                    if (reply.RoundtripTime >= _timeout)
                    {
                        return HealthCheckResult.Degraded($"Ping check for {_host} takes too long to respond. Expected {_timeout} ms but responded in {reply.RoundtripTime} ms.");
                    }

                    return HealthCheckResult.Healthy($"Ping check for {_host} is ok.");
                }
            }
            catch
            {
                return HealthCheckResult.Unhealthy($"Error when trying to check ping for {_host}.");
            }
        }
    }
}

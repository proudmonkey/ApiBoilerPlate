
using ApiBoilerPlate.Constants;
using ApiBoilerPlate.Contracts;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiBoilerPlate.Services
{
    public class AuthServerConnect : IAuthServerConnect
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthServerConnect> _logger;

        public AuthServerConnect(HttpClient httpClient, IConfiguration config, ILogger<AuthServerConnect> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
        }
        public async Task<(string AccessToken, string ErrorMessage)> RequestAccessToken()
        {
            string errorMessage = string.Empty;

            var endPointDiscovery = await _httpClient.GetDiscoveryDocumentAsync(_config["AuthServer:BaseUrl"]);
            if (endPointDiscovery.IsError)
            {
                errorMessage = $"ErrorType: {endPointDiscovery.ErrorType} Error: {endPointDiscovery.Error}";
                _logger.Log(LogLevel.Error, errorMessage);

                return (string.Empty, errorMessage);
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endPointDiscovery.TokenEndpoint,
                ClientId = _config["Self:Id"],
                ClientSecret = _config["Self:Secret"],
                
                //Add Scope here when neccessary
            });

            if (tokenResponse.IsError)
            {
                errorMessage = $"ErrorType: {tokenResponse.ErrorType} Error: {tokenResponse.Error}";

                _logger.Log(LogLevel.Error, errorMessage);
                return (string.Empty, errorMessage);
            }

            return (tokenResponse.AccessToken, errorMessage);
        }
    }
}

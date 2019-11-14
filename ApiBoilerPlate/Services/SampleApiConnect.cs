using ApiBoilerPlate.Constants;
using ApiBoilerPlate.Contracts;
using AutoWrapper.Wrappers;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace ApiBoilerPlate.Services
{
    public class SampleApiConnect: IApiConnect<ApiResponse>
    {
        private readonly HttpClient _httpClient;
        public SampleApiConnect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> PostAsync(string apiEndPoint, StringContent content, string token)
        {
            _httpClient.SetBearerToken(token);
            var httpResponse = await _httpClient.PostAsync(apiEndPoint, content);

            var contentResult = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ApiResponse>(contentResult);
            return response;
        }
    }
}

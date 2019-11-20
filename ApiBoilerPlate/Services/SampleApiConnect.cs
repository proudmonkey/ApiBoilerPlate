using ApiBoilerPlate.Contracts;
using AutoWrapper.Wrappers;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ApiBoilerPlate.DTO.Response;
using ApiBoilerPlate.DTO.Request;
using System.Text;
using ApiBoilerPlate.Constants;

namespace ApiBoilerPlate.Services
{
    public class SampleApiConnect: IApiConnect
    {
        private readonly HttpClient _httpClient;
        public SampleApiConnect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SampleResponse> PostDataAsync<SampleResponse, SampleRequest>(string endPoint, SampleRequest dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, HttpContentMediaTypes.JSON);
            var httpResponse = await _httpClient.PostAsync(endPoint, content);

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiException("An error occured while requesting external api", (int)httpResponse.StatusCode);

            var contentResult = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<SampleResponse>(contentResult);

            return response;
        }

        public async Task<SampleResponse> GetDataAsync<SampleResponse>(string endPoint)
        {
            var httpResponse = await _httpClient.GetAsync(endPoint);

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiException("An error occured while requesting external api", 500);

            var contentResult = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<SampleResponse>(contentResult);

            return response;
        }

    }
}

using ApiBoilerPlate.Contracts;
using AutoWrapper.Wrappers;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ApiBoilerPlate.DTO;
using System.Text;
using ApiBoilerPlate.Constants;

namespace ApiBoilerPlate.Services
{
    public class SampleApiConnect: IApiConnect<ApiResponse>
    {
        private readonly HttpClient _httpClient;
        public SampleApiConnect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> CreateSampleData(PersonDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, HttpContentMediaTypes.JSON);
            var httpResponse = await _httpClient.PostAsync("api/v1/sample", content);

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiException("An error occured while requesting external api", 500);

            var contentResult = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ApiResponse>(contentResult);

            return response;
        }

        public async Task<ApiResponse> GetSampleData(long id)
        {
            var httpResponse = await _httpClient.GetAsync($"api/v1/sample/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                throw new ApiException("An error occured while requesting external api", 500);

            var contentResult = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ApiResponse>(contentResult);

            return response;
        }

    }
}

using ApiBoilerPlate.Contracts;
using AutoWrapper.Wrappers;
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

        //public async Task<ApiResponse> CreateSampleData(string apiEndPoint, StringContent content)
        //{
        //    var httpResponse = await _httpClient.PostAsync(apiEndPoint, content);
        //    var contentResult = await httpResponse.Content.ReadAsStringAsync();
        //    var response = JsonConvert.DeserializeObject<ApiResponse>(contentResult);

        //    return response;
        //}

        public async Task<ApiResponse> GetSampleData()
        {
            var httpResponseString = await _httpClient.GetStringAsync("api/v1/sample");
            var response = JsonConvert.DeserializeObject<ApiResponse>(httpResponseString);

            return response;
        }

    }
}

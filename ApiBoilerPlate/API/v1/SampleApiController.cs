using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.DTO.Request;
using ApiBoilerPlate.DTO.Response;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiBoilerPlate.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SampleApiController: ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IApiConnect _sampleApiConnect;

        public SampleApiController(IApiConnect sampleApiConnect, ILogger<PersonsController> logger)
        {
            _sampleApiConnect = sampleApiConnect;
            _logger = logger;
        }

        [Route("{id:long}")]
        [HttpGet]
        public async Task<ApiResponse> Get(long id)
        {
            if (ModelState.IsValid)
                return new ApiResponse(await _sampleApiConnect.GetDataAsync<SampleResponse>($"/api/v1/sample/{id}"));
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] SampleRequest dto)
        {
            if (ModelState.IsValid)
                return  new ApiResponse(await _sampleApiConnect.PostDataAsync<SampleResponse,SampleRequest>("/api/v1/sample", dto));

            else
                throw new ApiException(ModelState.AllErrors());
        }
    }
}

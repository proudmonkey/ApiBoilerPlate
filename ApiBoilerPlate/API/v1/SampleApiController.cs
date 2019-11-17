using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.DTO;
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
        private readonly IApiConnect<ApiResponse> _sampleApiConnect;

        public SampleApiController(IApiConnect<ApiResponse> sampleApiConnect, ILogger<PersonsController> logger)
        {
            _sampleApiConnect = sampleApiConnect;
            _logger = logger;
        }

        [Route("{id:long}")]
        [HttpGet]
        public async Task<ApiResponse> Get(long id)
        {
            if (ModelState.IsValid)
                return await _sampleApiConnect.GetSampleData(id);
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonDTO dto)
        {
            if (ModelState.IsValid)
            {
                return await _sampleApiConnect.CreateSampleData(dto);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }
    }
}

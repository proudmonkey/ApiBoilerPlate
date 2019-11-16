using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Domain.Entity;
using ApiBoilerPlate.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using ApiBoilerPlate.Constants;
using Newtonsoft.Json;

namespace ApiBoilerPlate.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //Uncomment to secure the Api 
    //[Authorize]
    public class SampleApiController: ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IApiConnect<ApiResponse> _sampleApiConnect;

        public SampleApiController(IApiConnect<ApiResponse> sampleApiConnect, ILogger<PersonsController> logger)
        {
            _sampleApiConnect = sampleApiConnect;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ApiResponse> Get()
        {

            if (ModelState.IsValid)
            {
                var result = await _sampleApiConnect.GetSampleData();

                if (result.IsError)
                    throw new ApiException("An error occured while external api", 500);

                return result;
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }
    }
}

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
    [Authorize]
    public class SampleApiController: ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IApiConnect<ApiResponse> _sampleApiConnect;
        private readonly IAuthServerConnect _authServerConnect;

        public SampleApiController(IApiConnect<ApiResponse> sampleApiConnect, IAuthServerConnect authServerConnect, ILogger<PersonsController> logger)
        {
            _sampleApiConnect = sampleApiConnect;
            _authServerConnect = authServerConnect;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonDTO dto)
        {

            if (ModelState.IsValid)
            {
                var authServerResponse = await _authServerConnect.RequestAccessToken();

                if (string.IsNullOrEmpty(authServerResponse.AccessToken))
                    throw new ApiException(authServerResponse.ErrorMessage, 401);

                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, HttpContentMediaTypes.JSON);
                return await _sampleApiConnect.PostAsync("api/v1/SomeEndPoint", content, authServerResponse.AccessToken);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }
    }
}

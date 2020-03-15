using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO.Request;
using ApiBoilerPlate.DTO.Response;
using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ApiBoilerPlate.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonManager _personManager;
        private readonly IMapper _mapper;
        public PersonsController(IPersonManager personManager, IMapper mapper, ILogger<PersonsController> logger)
        {
            _personManager = personManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonQueryResponse>), Status200OK)]
        public async Task<IEnumerable<PersonQueryResponse>> Get()
        {
            var data = await _personManager.GetAllAsync();
            var persons = _mapper.Map<IEnumerable<PersonQueryResponse>>(data);

            return persons;
        }

        [Route("paged")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonQueryResponse>), Status200OK)]
        public async Task<IEnumerable<PersonQueryResponse>> Get([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            var data =  await _personManager.GetPersonsAsync(urlQueryParameters);
            var persons = _mapper.Map<IEnumerable<PersonQueryResponse>>(data.Persons);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.Pagination));

            return persons;
        }

        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(PersonQueryResponse), Status200OK)]
        [ProducesResponseType(typeof(PersonQueryResponse), Status404NotFound)]
        public async Task<PersonQueryResponse> Get(long id)
        {
            var person = await _personManager.GetByIdAsync(id);
            return person != null ? _mapper.Map<PersonQueryResponse>(person) 
                                  : throw new ApiProblemDetailsException($"Record with id: {id} does not exist.", Status404NotFound);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] CreatePersonRequest createRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState);  }

            var person = _mapper.Map<Person>(createRequest);
            return new ApiResponse("Record successfully created.", await _personManager.CreateAsync(person), Status201Created);     
        }

        [Route("{id:long}")]
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Put(long id, [FromBody] UpdatePersonRequest updateRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var person = _mapper.Map<Person>(updateRequest);
            person.Id = id;

            if (await _personManager.UpdateAsync(person)) { 
                return new ApiResponse($"Record with Id: {id} sucessfully updated.", true); 
            }
            else { 
                throw new ApiProblemDetailsException($"Record with Id: {id} does not exist.", Status404NotFound); 
            }
        }


        [Route("{id:long}")]
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        public async Task<ApiResponse> Delete(long id)
        {
            if (await _personManager.DeleteAsync(id)) { 
                return new ApiResponse($"Record with Id: {id} sucessfully deleted.", true); 
            }
            else { 
                throw new ApiProblemDetailsException($"Record with id: {id} does not exist.", Status404NotFound); 
            }
        }
    }
}
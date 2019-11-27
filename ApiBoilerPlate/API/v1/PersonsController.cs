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
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ApiBoilerPlate.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //Uncomment to secure the Api 
    //[Authorize]
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
        public async Task<IEnumerable<PersonResponse>> Get()
        {
            var data = await _personManager.GetAllAsync();
            var persons = _mapper.Map<IEnumerable<PersonResponse>>(data);

            return persons;
        }

        [Route("paged")]
        [HttpGet]
        public async Task<IEnumerable<PersonResponse>> Get([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            var data =  await _personManager.GetPersonsAsync(urlQueryParameters);
            var persons = _mapper.Map<IEnumerable<PersonResponse>>(data.Persons);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(data.Pagination));

            return persons;
        }

        [Route("{id:long}")]
        [HttpGet]
        public async Task<PersonResponse> Get(long id)
        {
            var data = await _personManager.GetByIdAsync(id);

            if (data != null)
            {
                var person = _mapper.Map<PersonResponse>(data);
                return person;
            }
            else
                throw new ApiException($"Record with id: {id} does not exist.", Status404NotFound);
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] CreatePersonRequest dto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var person = _mapper.Map<Person>(dto);
                    return new ApiResponse("Created Successfully", await _personManager.CreateAsync(person), Status201Created);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, "Error when trying to insert.");
                    throw;
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [Route("{id:long}")]
        [HttpPut]
        public async Task<ApiResponse> Put(long id, [FromBody] UpdatePersonRequest dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var person = _mapper.Map<Person>(dto);
                    person.ID = id;

                    if (await _personManager.UpdateAsync(person))
                        return new ApiResponse($"Record with Id: {id} sucessfully updated.", true);
                    else
                        throw new ApiException($"Record with Id: {id} does not exist.", Status404NotFound);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, "Error when trying to update the database with Id:{@ID}", id);
                    throw;
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }


        [Route("{id:long}")]
        [HttpDelete]
        public async Task<bool> Delete(long id)
        {
            try
            {
                if (await _personManager.DeleteAsync(id))
                    return true;
                else
                    throw new ApiException($"Record with id: {id} does not exist.", Status404NotFound);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error when trying to perform delete in database with Id:{@ID}", id);
                throw;
            }
        }
    }
}
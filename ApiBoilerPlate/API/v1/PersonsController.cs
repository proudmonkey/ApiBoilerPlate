using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Person>> Get()
        {
            return await _personManager.GetAllAsync();
        }

        [Route("{id:long}")]
        [HttpGet]
        public async Task<Person> Get(long id)
        {
            var person = await _personManager.GetByIdAsync(id);
            if (person != null)
            {
                return person;
            }
            else
                throw new ApiException($"Record with id: {id} does not exist.", 204);
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonDTO dto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var person = _mapper.Map<Person>(dto);
                    return new ApiResponse("Created Successfully", await _personManager.CreateAsync(person), 201);
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
        public async Task<ApiResponse> Put(long id, [FromBody] PersonDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var person = _mapper.Map<Person>(dto);
                    person.ID = id;

                    if (await _personManager.UpdateAsync(person))
                        return new ApiResponse("Update successful.", true);
                    else
                        throw new ApiException($"Record with id: {id} does not exist.", 400);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex, "Error when trying to update with ID:{@ID}", id);
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
                var isDeleted = await _personManager.DeleteAsync(id);
                if (isDeleted)
                {
                    return isDeleted;
                }
                else
                    throw new ApiException($"Record with id: {id} does not exist.", 400);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error when trying to delete with ID:{@ID}", id);
                throw;
            }

        }
    }
}
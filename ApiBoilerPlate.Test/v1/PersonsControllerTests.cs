using ApiBoilerPlate.API.v1;
using ApiBoilerPlate.Contracts;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO.Response;
using ApiBoilerPlate.DTO.Request;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ApiBoilerPlate.Test.v1
{
    public class PersonsControllerTests
    {
        private readonly Mock<IPersonManager> _mockDataManager;
        private readonly Mock<IMapper> _mapper;
        private readonly PersonsController _controller;

        public PersonsControllerTests()
        {
            var logger = Mock.Of<ILogger<PersonsController>>();

            _mapper = new Mock<IMapper>();
            _mockDataManager = new Mock<IPersonManager>();

            _controller = new PersonsController(_mockDataManager.Object, _mapper.Object, logger);
        }

        private IEnumerable<Person> GetFakePersonLists()
        {
            return new List<Person>
            {
                new Person()
                {
                    ID = 1,
                    FirstName = "Vynn Markus",
                    LastName = "Durano",
                    DateOfBirth = Convert.ToDateTime("01/15/2016")
                },
                new Person()
                {
                    ID = 2,
                    FirstName = "Vianne Maverich",
                    LastName = "Durano",
                    DateOfBirth = Convert.ToDateTime("02/15/2016")
                }
            };
        }

        [Fact]
        public async Task GET_All_RETURNS_OK()
        {

            // Arrange
            _mockDataManager.Setup(manager => manager.GetAllAsync())
               .ReturnsAsync(GetFakePersonLists());

            // Act
            var result = await _controller.Get();

            // Assert
            var persons = Assert.IsType<List<Person>>(result);
            Assert.Equal(2, persons.Count);
        }

        [Fact]
        public async Task GET_ById_RETURNS_OK()
        {
            long id = 1;

            _mockDataManager.Setup(manager => manager.GetByIdAsync(id))
               .ReturnsAsync(GetFakePersonLists().Single(p => p.ID.Equals(id)));

            var person = await _controller.Get(id);
            Assert.IsType<Person>(person);
        }

        [Fact]
        public async Task GET_ById_RETURNS_NOTFOUND()
        {
            await Assert.ThrowsAsync<ApiException>(() => _controller.Get(1));
        }

        [Fact]
        public async Task POST_Create_RETURNS_BADREQUEST()
        {
            // Arrange
            var dobMissingAttribute = new CreatePersonRequest()
            {
                FirstName = "Vinz",
                LastName = "Durano"
            };

            // Act
            _controller.ModelState.AddModelError("DateOfBirth", "Required");

            // Assert

            await Assert.ThrowsAsync<ApiException>(() => _controller.Post(dobMissingAttribute));
        }

        [Fact]
        public async Task POST_Create_RETURNS_OK()
        {
            // Arrange
            var dto = new CreatePersonRequest()
            {
                FirstName = "Vinz",
                LastName = "Durano",
                DateOfBirth = Convert.ToDateTime("02/15/2016")
            };

            var personEntity = new Person()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth
            };

            _mockDataManager.Setup(manager => manager.CreateAsync(personEntity))
                .ReturnsAsync(It.IsAny<long>());

            // Act
            var person = await _controller.Post(dto);

            // Assert
            Assert.IsType<ApiResponse>(person);
        }
    }
}

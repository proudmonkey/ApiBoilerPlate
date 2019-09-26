using AutoMapper;
using ApiBoilerPlate.Domain.Entity;
using ApiBoilerPlate.DTO;

namespace ApiBoilerPlate.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}

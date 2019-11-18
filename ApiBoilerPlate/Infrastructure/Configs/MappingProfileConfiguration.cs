using AutoMapper;
using ApiBoilerPlate.Data.Entity;
using ApiBoilerPlate.DTO;

namespace ApiBoilerPlate.Infrastructure.Configs
{
    public class MappingProfileConfiguration: Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}

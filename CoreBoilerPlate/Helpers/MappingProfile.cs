using AutoMapper;
using CoreBoilerPlate.Domain.Entity;
using CoreBoilerPlate.DTO;

namespace CoreBoilerPlate.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}

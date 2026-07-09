using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.DTOs;

namespace Web.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HRRole, HRRoleDto>().ReverseMap();
        }
    }
}
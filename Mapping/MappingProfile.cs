using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.DTOs;
using Web.API.Models.Entities;
using Web.API.Models.DTOS;

namespace Web.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HRRole, HRRoleDto>().ReverseMap();
            CreateMap<HRBranch, HRBranchDto>().ReverseMap();
            CreateMap<HRCompany, HRCompanyDto>().ReverseMap();
            CreateMap<HREmployee, HREmployeeDto>().ReverseMap();
            CreateMap<HRCorporateTitle, HRCorporateTitleDto>().ReverseMap();
            CreateMap<HRFunctionalTitle, HRFunctionalTitleDto>().ReverseMap();
        }
    }
}
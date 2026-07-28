using AutoMapper;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;

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
            CreateMap<HRPermission, HRPermissionDto>().ReverseMap();
        }
    }
}
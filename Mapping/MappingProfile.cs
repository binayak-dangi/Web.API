using AutoMapper;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;

namespace Web.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HRRole, HRRoleDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HRBranch, HRBranchDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HRCompany, HRCompanyDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HREmployee, HREmployeeDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HRCorporateTitle, HRCorporateTitleDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HRFunctionalTitle, HRFunctionalTitleDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<HRPermission, HRPermissionDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AdmMainHeading, AdmMainHeadingDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AdmHeading, AdmHeadingDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AdmElement, AdmElementDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
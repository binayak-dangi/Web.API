using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRCompanyService: IGenericService<HRCompanyDto>
    {
        Task<List<HRCompanyDto>> GetAllCompaniesAsync();
        Task<HRCompanyDto> GetCompanyByIdAsync(long companyId);
        Task<HRCompanyDto> CreateCompanyAsync(HRCompanyDto company);
        Task<HRCompanyDto> UpdateCompanyAsync(HRCompanyDto company);
        Task<HRCompanyDto> DeleteCompanyAsync(long companyId);
    }
}

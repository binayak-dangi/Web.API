using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Implementation;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRCompanyService : GenericService<HRCompany, HRCompanyDto>, IHRCompanyService
    {
        public HRCompanyService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRCompanyDto>> GetAllCompaniesAsync()
            => GetAllAsync();

        public Task<HRCompanyDto?> GetCompanyByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRCompanyDto> CreateCompanyAsync(HRCompanyDto dto)
            => CreateAsync(dto);

        public Task<HRCompanyDto?> UpdateCompanyAsync(HRCompanyDto dto)
            => UpdateAsync(dto);

        public Task<HRCompanyDto> DeleteCompanyAsync(long id)
            => SoftDeleteAsync(id);
    }
}
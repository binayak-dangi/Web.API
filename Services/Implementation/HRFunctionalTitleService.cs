using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Models.Entities;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Implementation;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRFunctionalTitleService : GenericService<HRFunctionalTitle, HRFunctionalTitleDto>, IHRFunctionalTitleService
    {
        public HRFunctionalTitleService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRFunctionalTitleDto>> GetAllFunctionalTitlesAsync()
            => GetAllAsync();

        public Task<HRFunctionalTitleDto?> GetFunctionalTitleByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRFunctionalTitleDto> CreateFunctionalTitleAsync(HRFunctionalTitleDto dto)
            => CreateAsync(dto);

        public Task<HRFunctionalTitleDto?> UpdateFunctionalTitleAsync(HRFunctionalTitleDto dto)
            => UpdateAsync(dto);

        public Task<HRFunctionalTitleDto> DeleteFunctionalTitleAsync(long id)
            => SoftDeleteAsync(id);
    }
}
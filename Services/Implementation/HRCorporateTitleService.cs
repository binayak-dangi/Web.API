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
    public class HRCorporateTitleService : GenericService<HRCorporateTitle, HRCorporateTitleDto>, IHRCorporateTitleService
    {
        public HRCorporateTitleService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRCorporateTitleDto>> GetAllCorporateTitlesAsync()
            => GetAllAsync();

        public Task<HRCorporateTitleDto?> GetCorporateTitleByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRCorporateTitleDto> CreateCorporateTitleAsync(HRCorporateTitleDto dto)
            => CreateAsync(dto);

        public Task<HRCorporateTitleDto?> UpdateCorporateTitleAsync(HRCorporateTitleDto dto)
            => UpdateAsync(dto);

        public Task<HRCorporateTitleDto> DeleteCorporateTitleAsync(long id)
            => SoftDeleteAsync(id);
    }
}
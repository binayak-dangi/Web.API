using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRCorporateTitleService: IGenericService<HRCorporateTitleDto>
    {
        Task<List<HRCorporateTitleDto>> GetAllCorporateTitlesAsync();
        Task<HRCorporateTitleDto> GetCorporateTitleByIdAsync(long corporateTitleId);
        Task<HRCorporateTitleDto> CreateCorporateTitleAsync(HRCorporateTitleDto corporateTitle);
        Task<HRCorporateTitleDto> UpdateCorporateTitleAsync(HRCorporateTitleDto corporateTitle);
        Task<HRCorporateTitleDto> DeleteCorporateTitleAsync(long corporateTitleId);
    }
}

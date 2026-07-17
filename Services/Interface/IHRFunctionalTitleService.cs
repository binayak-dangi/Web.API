using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRFunctionalTitleService: IGenericService<HRFunctionalTitleDto>
    {
        Task<List<HRFunctionalTitleDto>> GetAllFunctionalTitlesAsync();
        Task<HRFunctionalTitleDto> GetFunctionalTitleByIdAsync(long functionalTitleId);
        Task<HRFunctionalTitleDto> CreateFunctionalTitleAsync(HRFunctionalTitleDto functionalTitle);
        Task<HRFunctionalTitleDto> UpdateFunctionalTitleAsync(HRFunctionalTitleDto functionalTitle);
        Task<HRFunctionalTitleDto> DeleteFunctionalTitleAsync(long functionalTitleId);
    }
}

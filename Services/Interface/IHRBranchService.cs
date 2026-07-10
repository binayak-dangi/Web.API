using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRBranchService: IGenericService<HRBranchDto>
    {
        Task<List<HRBranchDto>> GetAllBranchesAsync();
        Task<HRBranchDto> GetBranchByIdAsync(long branchId);
        Task<HRBranchDto> CreateBranchAsync(HRBranchDto branch);
        Task<HRBranchDto> UpdateBranchAsync(HRBranchDto branch);
        Task<HRBranchDto> DeleteBranchAsync(long branchId);
    }
}

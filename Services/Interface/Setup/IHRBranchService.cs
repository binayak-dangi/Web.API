
using Web.API.Models.DTOS.Setup;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface.Setup
{
    public interface IHRBranchService: IGenericService<HRBranchDto>
    {
        Task<bool> IsBranchExist(HRBranchDto dto);

    }
}

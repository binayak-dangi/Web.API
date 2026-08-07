using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Services.Common;

namespace Web.API.Services.Setup.Interfaces
{
    public interface IHRBranchService: IBaseService<HRBranchDto>
    {
        Task<bool> IsBranchExist(HRBranchDto dto);

    }
}

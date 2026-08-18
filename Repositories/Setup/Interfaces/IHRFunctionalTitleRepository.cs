using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHRFunctionalTitleRepository: IBaseRepository<HRFunctionalTitleDto>
    {
        Task<bool> IsFunctionalTitleExist(long? id, HRFunctionalTitleDto dto);
    }
}

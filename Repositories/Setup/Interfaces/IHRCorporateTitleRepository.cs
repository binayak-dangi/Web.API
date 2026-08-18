using Web.API.Models.DTOS;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHRCorporateTitleRepository: IBaseRepository<HRCorporateTitleDto>
    {
        Task<bool> IsCorporateTitleExist(long? id, HRCorporateTitleDto dto);
    }
}

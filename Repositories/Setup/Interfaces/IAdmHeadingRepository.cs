using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IAdmHeadingRepository : IBaseRepository< AdmHeadingDto>
    {
        Task<bool> IsHeadingExist(long? id, AdmHeadingDto dto);
    }
}

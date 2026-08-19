using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IAdmMainHeadingRepository : IBaseRepository< AdmMainHeadingDto>
    {
        Task<bool> IsMainHeadingExist(long? id, AdmMainHeadingDto dto);
    }
}

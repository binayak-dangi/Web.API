using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IAdmElementRepository : IBaseRepository< AdmElementDto>
    {
        Task<bool> IsElementExist(long? id, AdmElementDto dto);
    }
}

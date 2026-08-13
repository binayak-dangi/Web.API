using Web.API.Models.Entities.Setup;

namespace Web.API.Repositories.Setup.Interfaces;

public interface IActivityLogRepository
{
    Task LogAsync(ActivityLog activityLog);
}
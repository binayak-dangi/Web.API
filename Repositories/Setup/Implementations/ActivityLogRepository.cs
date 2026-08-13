using Web.API.Data;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations;

public class ActivityLogRepository : IActivityLogRepository
{
    private readonly AppDbContext _context;

    public ActivityLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(ActivityLog activityLog)
    {
        _context.ActivityLog.Add(activityLog);

        await _context.SaveChangesAsync();
    }
}
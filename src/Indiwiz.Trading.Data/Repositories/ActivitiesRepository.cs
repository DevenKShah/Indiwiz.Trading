using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

internal class ActivitiesRepository : IActivitiesRepository
{
    private readonly TradingDataContext _dataContext;

    public ActivitiesRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddActivities(List<Activity> activities) => await _dataContext.Activities.AddRangeAsync(activities);

    public async Task<List<Activity>> GetAllActivities() => await _dataContext.Activities.ToListAsync();

    public async Task<List<Activity>> GetAllActivityByType(ActivityType activityType)
    {
        return await _dataContext
            .Activities
            .AsNoTracking()
            .Where(t => t.ActivityType == activityType)
            .OrderByDescending(t => t.TimeStamp)
            .ToListAsync();
    }
}

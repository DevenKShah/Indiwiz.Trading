using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IActivitiesRepository
{
    Task<List<Activity>> GetAllActivities();
    Task<List<Activity>> GetAllActivityByType(ActivityType activityType);
    Task AddActivities(List<Activity> activities);
}

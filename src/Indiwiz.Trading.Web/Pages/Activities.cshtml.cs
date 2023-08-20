using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages
{
    public class ActivitiesModel : PageModel
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ActivitiesModel(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public ActivityType ActivityType { get; set; }

        public string PageHeader =>
            this.ActivityType switch
            {
                ActivityType.Topup => "Investments",
                ActivityType.InterestFromCash => "Interest received",
                ActivityType.Withdrawal => "Withdrawals",
                ActivityType.Dividend => "Dividend received",
                _ => throw new NotImplementedException()
            };

        public List<Activity> Activities { get; set; } = new();

        public async Task OnGetAsync(string activityType)
        {
            this.ActivityType = Enum.Parse<ActivityType>(activityType, true);
            Activities = await _activitiesRepository.GetAllActivityByType(ActivityType);
        }
    }
}

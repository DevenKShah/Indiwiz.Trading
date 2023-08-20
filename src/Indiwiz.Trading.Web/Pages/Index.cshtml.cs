using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Indiwiz.Trading.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IOrdersRepository _orderRepository;
    private readonly IInstrumentsRepository _instrumentsRepository;
    private readonly IActivitiesRepository _activitiesRepository;

    public IndexModel(IOrdersRepository orderRepository, IInstrumentsRepository instrumentsRepository, IActivitiesRepository activitiesRepository)
    {
        _orderRepository = orderRepository;
        _instrumentsRepository = instrumentsRepository;
        _activitiesRepository = activitiesRepository;
    }

    public DateTime LastActivityDate { get; set; }

    public List<Card> Cards { get; set; } = new();

    [BindProperty]
    public long SelectedInstrumentId { get; set; }
    public SelectList Instruments { get; set; } = null!;

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;

        var activities = await _activitiesRepository.GetAllActivities();

        Cards.Add(new(ActivityType.Topup, "Investments", activities.Where(a => a.ActivityType == ActivityType.Topup).Sum(i => i.Amount).ToString("C")));
        Cards.Add(new(ActivityType.InterestFromCash, "Interests", activities.Where(a => a.ActivityType == ActivityType.InterestFromCash).Sum(i => i.Amount).ToString("C")));
        Cards.Add(new(ActivityType.Withdrawal, "Withdrawals", activities.Where(a => a.ActivityType == ActivityType.Withdrawal).Sum(i => i.Amount).ToString("C")));
        Cards.Add(new(ActivityType.Dividend, "Dividends", activities.Where(a => a.ActivityType == ActivityType.Dividend).Sum(i => i.Amount).ToString("C")));

        await PopulateInstruments();
    }

    public async Task PopulateInstruments()
    {
        var instruments = await _instrumentsRepository.GetInstruments();
        Instruments = new(instruments.OrderBy(i => i.Title), nameof(Instrument.Id), nameof(Instrument.Title));
    }

    public IActionResult OnPost()
    {
        return new RedirectToPageResult("InstrumentDetails", new { instrumentId = SelectedInstrumentId });
    }
}

public record Card(ActivityType ActivityType, string Header, string Body);


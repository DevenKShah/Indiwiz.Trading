using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Indiwiz.Trading.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IOrdersRepository _orderRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly IInvestmentsRepository _investmentsRepository;
    private readonly IInstrumentsRepository _instrumentsRepository;

    public IndexModel(IOrdersRepository orderRepository, IInterestRepository interestRepository, IInvestmentsRepository investmentsRepository, IInstrumentsRepository instrumentsRepository)
    {
        _orderRepository = orderRepository;
        _interestRepository = interestRepository;
        _investmentsRepository = investmentsRepository;
        _instrumentsRepository = instrumentsRepository;
    }

    public DateTime LastActivityDate { get; set; }

    public List<Card> Cards { get; set; } = new();

    [BindProperty]
    public long SelectedInstrumentId { get; set; }
    public SelectList Instruments { get; set; }

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;

        var interests = await _interestRepository.GetAllInterests();

        Cards.Add(new("Interests", interests.Sum(i => i.Amount).ToString("C")));

        var investments = await _investmentsRepository.GetAllInvestments();

        Cards.Add(new("Investments", investments.Sum(i => i.Amount).ToString("C")));

        await PopulateInstruments();
    }

    public async Task PopulateInstruments()
    {
        var instruments = await _instrumentsRepository.GetInstruments();
        Instruments = new(instruments, nameof(Instrument.Id), nameof(Instrument.Title));
    }

    public IActionResult OnPost()
    {
        return new RedirectToPageResult("InstrumentDetails", new { instrumentId = SelectedInstrumentId });
    }
}

public record Card(string Header, string Body);


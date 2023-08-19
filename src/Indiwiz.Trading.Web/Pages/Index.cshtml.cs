using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IOrdersRepository _orderRepository;
    private readonly IInterestRepository _interestRepository;
    private readonly IInvestmentsRepository _investmentsRepository;

    public IndexModel(ILogger<IndexModel> logger, IOrdersRepository orderRepository, IInterestRepository interestRepository, IInvestmentsRepository investmentsRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _interestRepository = interestRepository;
        _investmentsRepository = investmentsRepository;
    }

    public DateTime LastActivityDate { get; set; }

    public List<Card> Cards { get; set; } = new();

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;

        var interests = await _interestRepository.GetAllInterests();

        Cards.Add(new("Interests", interests.Sum(i => i.Amount).ToString("C")));

        var investments = await _investmentsRepository.GetAllInvestments();

        Cards.Add(new("Investments", investments.Sum(i => i.Amount).ToString("C")));
    }
}

public record Card(string Header, string Body);


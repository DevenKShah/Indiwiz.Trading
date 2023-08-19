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
    public decimal InterestEarnedSoFar { get; set; }
    public decimal InvestmentsMadeSoFar { get; set; }

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;

        var interests = await _interestRepository.GetAllInterests();
        InterestEarnedSoFar = interests.Sum(i => i.Amount);

        var investments = await _investmentsRepository.GetAllInvestments();
        InvestmentsMadeSoFar = investments.Sum(i => i.Amount);
    }
}

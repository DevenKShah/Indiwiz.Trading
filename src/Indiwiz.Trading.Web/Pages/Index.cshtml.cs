using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IInterestRepository _interestRepository;

    public DateTime LastActivityDate { get; set; }
    public decimal InterestEarnedSoFar { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IOrderRepository orderRepository, IInterestRepository interestRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _interestRepository = interestRepository;
    }

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;

        var interests = await _interestRepository.GetAllInterests();
        InterestEarnedSoFar = interests.Sum(i => i.Amount);
    }
}

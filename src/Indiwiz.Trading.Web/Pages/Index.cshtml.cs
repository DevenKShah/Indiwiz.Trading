using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IOrderRepository _orderRepository;

    public DateTime LastActivityDate { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task OnGetAsync()
    {
        var order = await _orderRepository.GetLatestOrder();
        LastActivityDate = order.OrderDate;
    }
}

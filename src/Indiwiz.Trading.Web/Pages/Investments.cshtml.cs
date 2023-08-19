using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InvestmentsModel : PageModel
{
    private readonly IInvestmentsRepository _investmentsRepository;

    public InvestmentsModel(IInvestmentsRepository investmentsRepository)
    {
        _investmentsRepository = investmentsRepository;
    }

    public List<Investment> Investments { get; set; } = new();

    public async Task OnGetAsync()
    {
        Investments = await _investmentsRepository.GetAllInvestments();
    }
}

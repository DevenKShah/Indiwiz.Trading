using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InterestsModel : PageModel
{
    private readonly IInterestRepository _interestRepository;

    public InterestsModel(IInterestRepository interestRepository)
    {
        _interestRepository = interestRepository;
    }

    public List<Interest> Interests { get; set; }

    public async Task OnGetAsync()
    {
        Interests = await _interestRepository.GetAllInterests();
    }
}

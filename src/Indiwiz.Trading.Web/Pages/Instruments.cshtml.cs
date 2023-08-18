using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentsModel : PageModel
{
    private IInstrumentsRepository _instrumentsRepository;

    public IEnumerable<Instrument> Instruments { get; set; }

    public InstrumentsModel(IInstrumentsRepository instrumentsRepository)
    {
        _instrumentsRepository = instrumentsRepository;
    }

    public async Task OnGet()
    {
        Instruments = await _instrumentsRepository.GetInstruments();
    }
}

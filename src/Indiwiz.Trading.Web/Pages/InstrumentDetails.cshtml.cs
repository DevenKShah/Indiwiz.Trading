using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentDetailsModel : PageModel
{
    private readonly IInstrumentsRepository _instrumentsRepository;

    public Instrument Instrument { get; set; }

    public InstrumentDetailsModel(IInstrumentsRepository instrumentsRepository)
    {
        _instrumentsRepository = instrumentsRepository;
    }

    public async Task OnGetAsync(long instrumentId)
    {
        Instrument = await _instrumentsRepository.GetInstrumentById(instrumentId);
    }
}

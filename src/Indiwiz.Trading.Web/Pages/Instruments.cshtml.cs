using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentsModel : PageModel
{
    private readonly IInstrumentsRepository _instrumentsRepository;

    public SelectList Instruments { get; set; } = null!;

    [BindProperty]
    public long SelectedInstrumentId { get; set; }

    public InstrumentsModel(IInstrumentsRepository instrumentsRepository)
    {
        _instrumentsRepository = instrumentsRepository;
    }

    public async Task OnGetAsync()
    {
        var instruments = await _instrumentsRepository.GetInstruments();
        Instruments = new(instruments, nameof(Instrument.Id), nameof(Instrument.Title));
    }

    public IActionResult OnPost()
    {
        return new RedirectToPageResult("InstrumentDetails", new { instrumentId = SelectedInstrumentId });
    }
}

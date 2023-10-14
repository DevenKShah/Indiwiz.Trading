using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentDetailsModel : PageModel
{
    private readonly IInstrumentsRepository _instrumentsRepository;

    public Instrument Instrument { get; set; } = null!;

    public decimal CurrentPrice { get; set; }
    public decimal StockBought { get; set; }
    public decimal TotalBoughtAmount { get; set; }
    public decimal StockSold { get; set; }
    public decimal TotalSoldAmount { get; set; }
    public decimal StockInHand => StockBought - StockSold;
    public decimal StockInHandAmount => StockInHand * CurrentPrice;
    public decimal ProfitAndLoss => TotalSoldAmount + StockInHandAmount - TotalBoughtAmount;

    public InstrumentDetailsModel(IInstrumentsRepository instrumentsRepository)
    {
        _instrumentsRepository = instrumentsRepository;
    }

    public async Task OnGetAsync(long instrumentId)
    {
        Instrument = await _instrumentsRepository.GetInstrumentById(instrumentId);
        StockBought = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Buy).Sum(o => o.Quantity);
        TotalBoughtAmount = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Buy).Sum(o => o.AmountInAccountCurrency);
        StockSold = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Sell).Sum(o => o.Quantity);
        TotalSoldAmount = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Sell).Sum(o => o.AmountInAccountCurrency);
    }
}

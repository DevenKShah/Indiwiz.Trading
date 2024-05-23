using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentsModel : PageModel
{
    private readonly IInstrumentsRepository _instrumentsRepository;

    public IEnumerable<InstrumentSummary> Instruments { get; set; } = Enumerable.Empty<InstrumentSummary>();

    public InstrumentsModel(IInstrumentsRepository instrumentsRepository)
    {
        _instrumentsRepository = instrumentsRepository;
    }

    public async Task OnGetAsync()
    {
        var instruments = await _instrumentsRepository.GetInstruments();

        Instruments = instruments.Select(i => (InstrumentSummary)i);
    }
}

public record InstrumentSummary(long InstrumentId, string Title, decimal TotalBuyCount, decimal TotalBuyAmount, decimal TotalSellCount, decimal TotalSellAmount, decimal StockInHand, decimal AverageBuyPrice)
{
    public static implicit operator InstrumentSummary(Instrument source)
    {
        var buyOrders = source.Orders.Where(o => o.TransactionType == TransactionType.Buy);
        var sellOrders = source.Orders.Where(o => o.TransactionType == TransactionType.Sell);

        decimal totalBuyQuantity = buyOrders.Sum(o => o.Quantity);
        decimal totalSellQuantity = sellOrders.Sum(o => o.Quantity);
        decimal totalBuyAmount = buyOrders.Sum(o => o.AmountInAccountCurrency);
        decimal totalSellAmount = sellOrders.Sum(o => o.AmountInAccountCurrency);
        var avgPrice = buyOrders.Any() ? buyOrders.Average(b => b.RateInInstrumentCurrency) : 0;
        return new(source.Id, source.Title, totalBuyQuantity, totalBuyAmount, totalSellQuantity, totalSellAmount, totalBuyQuantity - totalSellQuantity, avgPrice);
    }
}

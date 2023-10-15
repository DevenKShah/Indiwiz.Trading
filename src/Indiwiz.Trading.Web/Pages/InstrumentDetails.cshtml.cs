using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indiwiz.Trading.Web.Pages;

public class InstrumentDetailsModel : PageModel
{
    private readonly IInstrumentsRepository _instrumentsRepository;
    private readonly IStockReaderService _stockReaderService;

    public Instrument Instrument { get; set; } = null!;

    public decimal CurrentPrice { get; set; }
    public decimal StockBought { get; set; }
    public decimal TotalBoughtAmount { get; set; }
    public decimal StockSold { get; set; }
    public decimal TotalSoldAmount { get; set; }
    public decimal StockInHand => StockBought - StockSold;
    public decimal StockInHandAmount => StockInHand * CurrentPrice;
    public decimal ProfitAndLoss => TotalSoldAmount + StockInHandAmount - TotalBoughtAmount;

    public InstrumentDetailsModel(IInstrumentsRepository instrumentsRepository, IStockReaderService stockReaderService)
    {
        _instrumentsRepository = instrumentsRepository;
        _stockReaderService = stockReaderService;
    }

    public async Task OnGetAsync(long instrumentId)
    {
        Instrument = await _instrumentsRepository.GetInstrumentById(instrumentId);
        StockBought = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Buy).Sum(o => o.Quantity);
        TotalBoughtAmount = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Buy).Sum(o => o.AmountInAccountCurrency);
        StockSold = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Sell).Sum(o => o.Quantity);
        TotalSoldAmount = Instrument.Orders.Where(o => o.TransactionType == TransactionType.Sell).Sum(o => o.AmountInAccountCurrency);

        CurrentPrice = await _stockReaderService.GetLastClosingPrice(Instrument.Ticker, GetExchange(Instrument.CurrencyName));

        if (CurrentPrice > 0 && Instrument.CurrencyName == CurrencyName.GBP) CurrentPrice = CurrentPrice / 100;
    }

    private static string? GetExchange(CurrencyName currencyName) =>
        currencyName switch
        {
            CurrencyName.GBP => "LON",
            CurrencyName.EUR => "DEX",
            CurrencyName.USD => null,
            _ => null
        };
}

namespace Indiwiz.Trading.Domain.Entities;

public class Instrument
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public CurrencyName Currency { get; set; }
    public string Ticker { get; set; } = null!;
    public string ISIN { get; set; } = null!;
    public virtual List<Order> Orders { get; set; } = new();
}

namespace Indiwiz.Trading.Domain.Entities;

public class Instrument : EntityBase
{
    public required string ISIN { get; set; }
    public required string Title { get; set; }
    public CurrencyName CurrencyName { get; set; }
    public required string Ticker { get; set; }
    public virtual List<Order> Orders { get; set; } = new();
}

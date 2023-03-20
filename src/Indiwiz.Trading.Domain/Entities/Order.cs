namespace Indiwiz.Trading.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int InvestmentId { get; set; }
    public OrderType OrderType { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public virtual Instrument Instrument { get; set; } = null!;
}
namespace Indiwiz.Trading.Domain.Entities;

public class Order : EntityBase
{
    public long InstrumentId { get; set; }
    public required string OrderId { get; set; }
    public TransactionType TransactionType { get; set; }
    public OrderType OrderType { get; set; }
    public decimal Quantity { get; set; }
    public decimal RateInInstrumentCurrency { get; set; }
    public decimal AmountInAccountCurrency { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Instrument Instrument { get; set; } = null!;
}
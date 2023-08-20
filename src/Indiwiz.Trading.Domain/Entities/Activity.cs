namespace Indiwiz.Trading.Domain.Entities;

public class Activity : EntityBase
{
    public ActivityType ActivityType { get; set; }
    public DateTime TimeStamp { get; set; }
    public decimal Amount { get; set; }
    public long? InstrumentId { get; set; }

    public Instrument? Instrument { get; set; }
}

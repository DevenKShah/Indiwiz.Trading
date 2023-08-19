namespace Indiwiz.Trading.Domain.Entities;

public class Interest : EntityBase
{
    public DateTime ReceivedDate { get; set; }

    public decimal Amount { get; set; }
}

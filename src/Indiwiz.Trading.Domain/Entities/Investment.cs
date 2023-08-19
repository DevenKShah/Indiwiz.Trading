namespace Indiwiz.Trading.Domain.Entities;

public class Investment : EntityBase
{
    public DateTime InvestmentDate { get; set; }
    public decimal Amount { get; set; }
}

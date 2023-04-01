using System.ComponentModel;

namespace Indiwiz.Trading.Domain.Entities;

public enum ActivityType
{
    Order,
    [Description("MONTHLY_STATEMENT")]
    MonthlyStatement,
    [Description("INTEREST_FROM_CASH")]
    InterestFromCash,
    Dividend,
    [Description("TOP_UP")]
    Topup,
    Withdrawal
}

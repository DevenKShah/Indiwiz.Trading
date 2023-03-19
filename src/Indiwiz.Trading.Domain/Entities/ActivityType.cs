using System.Collections.Concurrent;
namespace Indiwiz.Trading.Domain.Entities;

public enum ActivityType
{
    MonthlyStatement,
    Order,
    InterestFromCash,
    Dividend,
    TopUp,
    Withdrawal
}

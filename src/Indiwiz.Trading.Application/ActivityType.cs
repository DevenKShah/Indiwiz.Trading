using System.Collections.Concurrent;
namespace Indiwiz.Trading.Application;

public enum ActivityType
{
    MonthlyStatement,
    Order,
    InterestFromCash,
    Dividend,
    TopUp,
    Withdrawal
}

namespace Indiwiz.Trading.Domain.Interfaces;

public interface ITradingDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

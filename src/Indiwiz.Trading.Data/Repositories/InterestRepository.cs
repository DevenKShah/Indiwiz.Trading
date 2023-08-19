using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

internal class InterestRepository : IInterestRepository
{
    private readonly TradingDataContext _dataContext;

    public InterestRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Interest>> GetAllInterests() => await _dataContext.InterestReceived.ToListAsync();

    public async Task AddInterests(List<Interest> interests) => await _dataContext.InterestReceived.AddRangeAsync(interests);
}

using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

internal class InvestmentsRepository : IInvestmentsRepository
{
    private readonly TradingDataContext _dataContext;

    public InvestmentsRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddInvestments(List<Investment> investments) => await _dataContext.Investments.AddRangeAsync(investments);

    public async Task<List<Investment>> GetAllInvestments() => await _dataContext.Investments.ToListAsync();
}

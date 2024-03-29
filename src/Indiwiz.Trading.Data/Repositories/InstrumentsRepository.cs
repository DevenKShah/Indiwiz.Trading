using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

public class InstrumentsRepository : IInstrumentsRepository
{
    private readonly TradingDataContext _dataContext;
    public InstrumentsRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<List<Instrument>> GetInstruments() => await _dataContext.Instruments.Include(i => i.Orders).ToListAsync();

    public async Task<Instrument> GetInstrumentById(long instrumentId) =>
        await _dataContext
        .Instruments
        .Where(i => i.Id == instrumentId)
        .Include(i => i.Orders)
        .FirstAsync();

    public async Task AddInstruments(IEnumerable<Instrument> instruments) => await _dataContext.Instruments.AddRangeAsync(instruments);
    public async Task AddInstrument(Instrument instrument) => await _dataContext.Instruments.AddAsync(instrument);
}

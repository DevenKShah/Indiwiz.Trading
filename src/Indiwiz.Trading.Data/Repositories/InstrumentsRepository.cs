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
    public async Task<List<Instrument>> GetInstruments() => await _dataContext.Instruments.ToListAsync();
    public void AddInstruments(List<Instrument> instruments) => _dataContext.Instruments.AddRangeAsync(instruments);
    public void AddInstrument(Instrument instrument) => _dataContext.Instruments.AddAsync(instrument);
}

using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IInstrumentsRepository
{
    Task AddInstrument(Instrument instrument);
    Task AddInstruments(IEnumerable<Instrument> instruments);
    Task<List<Instrument>> GetInstruments();
}
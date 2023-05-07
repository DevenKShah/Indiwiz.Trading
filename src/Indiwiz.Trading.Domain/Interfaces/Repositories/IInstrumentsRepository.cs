using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IInstrumentsRepository
{
    Task AddInstrument(Instrument instrument);
    Task AddInstruments(List<Instrument> instruments);
    Task<List<Instrument>> GetInstruments();
}
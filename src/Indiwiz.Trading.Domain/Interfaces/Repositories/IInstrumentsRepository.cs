using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IInstrumentsRepository
{
    void AddInstrument(Instrument instrument);
    void AddInstruments(List<Instrument> instruments);
    Task<List<Instrument>> GetInstruments();
}
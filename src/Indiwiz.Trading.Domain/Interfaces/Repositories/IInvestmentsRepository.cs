using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IInvestmentsRepository
{
    Task<List<Investment>> GetAllInvestments();
    Task AddInvestments(List<Investment> investments);
}

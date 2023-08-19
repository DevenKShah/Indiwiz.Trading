using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IInterestRepository
{
    Task<List<Interest>> GetAllInterests();
    Task AddInterests(List<Interest> interests);
}

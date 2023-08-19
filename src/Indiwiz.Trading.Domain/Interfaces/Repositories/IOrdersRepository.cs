using Indiwiz.Trading.Domain.Entities;

namespace Indiwiz.Trading.Domain.Interfaces.Repositories;

public interface IOrdersRepository
{
    Task<Order> GetLatestOrder();
}

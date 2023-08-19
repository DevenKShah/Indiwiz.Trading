using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

internal class OrderRepository : IOrderRepository
{
    private readonly TradingDataContext _dataContext;

    public OrderRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Order> GetLatestOrder() => await _dataContext.Orders.OrderByDescending(o => o.OrderDate).FirstAsync();
}

using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data.Repositories;

internal class OrdersRepository : IOrdersRepository
{
    private readonly TradingDataContext _dataContext;

    public OrdersRepository(TradingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Order> GetLatestOrder() => await _dataContext.Orders.OrderByDescending(o => o.OrderDate).FirstAsync();
}

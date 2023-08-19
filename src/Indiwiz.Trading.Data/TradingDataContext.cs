using Indiwiz.Trading.Data.Configurations;
using Indiwiz.Trading.Domain.Entities;
using Indiwiz.Trading.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data;

public class TradingDataContext : DbContext, ITradingDataContext
{
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<Order> Orders => Set<Order>();

    public TradingDataContext() { }
    public TradingDataContext(DbContextOptions<TradingDataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstrumentConfiguration).Assembly);
    }
}

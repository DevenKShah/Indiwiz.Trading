using Indiwiz.Trading.Data.Repositories;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Indiwiz.Trading.Data;

public static class AddDataRegistrationsExtension
{
    public static IServiceCollection AddDataRegistrations(this IServiceCollection services)
    {
        services.AddTransient<IInstrumentsRepository, InstrumentsRepository>();
        services.AddDbContext<TradingDataContext>();
        return services;
    }
}

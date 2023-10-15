using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;
using Microsoft.Extensions.DependencyInjection;

namespace Indiwiz.Trading.Infrastructure;

public static class AddInfrastructureRegistrationsExtension
{
    public static IServiceCollection AddInfrastructureRegistrations(this IServiceCollection services)
    {
        services.AddTransient<ILoadActivityDataService, LoadActivityDataService>();
        services.AddTransient<IStockReaderService, AlphaVantageService>();
        return services;
    }
}

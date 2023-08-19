using Indiwiz.Trading.Data.Repositories;
using Indiwiz.Trading.Domain.Interfaces;
using Indiwiz.Trading.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Indiwiz.Trading.Data.Extensions;

public static class AddDataRegistrationsExtension
{
    public static IServiceCollection AddDataRegistrations(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddDbContext<TradingDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString")));

        services.AddTransient<ITradingDataContext, TradingDataContext>(provider => provider.GetService<TradingDataContext>()!);
        services.AddTransient<IInstrumentsRepository, InstrumentsRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        return services;
    }
}

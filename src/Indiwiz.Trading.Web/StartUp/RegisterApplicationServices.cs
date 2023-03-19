using Indiwiz.Trading.Application.Services;
using Indiwiz.Trading.Domain.Interfaces;

namespace Indiwiz.Trading.Web.StartUp;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ILoadDataService, LoadDataService>();
        return services;
    }
}
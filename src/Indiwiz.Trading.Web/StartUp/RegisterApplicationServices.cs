using Indiwiz.Trading.Domain.Interfaces;
using Indiwiz.Trading.Infrastructure.Services.LoadActivityData;

namespace Indiwiz.Trading.Web.StartUp;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ILoadActivityDataService, LoadActivityDataService>();
        return services;
    }
}
using Indiwiz.Trading.Application.Services;

namespace Indiwiz.Trading.Web.StartUp;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(IServiceCollection services)
    {
        services.AddTransient<ILoadDataService, LoadDataService>();
        return services;
    }
}
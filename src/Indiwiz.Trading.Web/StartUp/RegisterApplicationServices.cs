using Indiwiz.Trading.Data;
using Indiwiz.Trading.Infrastructure;

namespace Indiwiz.Trading.Web.StartUp;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) => 
        services
            .AddInfrastructureRegistrations()
            .AddDataRegistrations();
}
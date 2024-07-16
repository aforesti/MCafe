using MCafe.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MCafe.Shared.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        ILogger logger)
    {
        services.AddSingleton(TimeProvider.System);

        logger.Information("Shared services added");

        return services;
    }
}

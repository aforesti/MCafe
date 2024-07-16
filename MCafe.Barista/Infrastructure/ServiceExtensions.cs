using MCafe.Barista.Infrastructure.Data;
using MCafe.Shared;
using MCafe.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MCafe.Barista.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddBaristaService(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        var connectionString = config.GetConnectionString("BaristaConnectionString");
        services.AddDbContext<BaristaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ServiceExtensions)));

        services.AddScoped<IRepository, Repository<BaristaDbContext>>();
        services.AddScoped<IReadOnlyRepository, Repository<BaristaDbContext>>();

        logger.Information("Barista service added");
        return services;
    }
}

using MCafe.Cashier.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MCafe.Cashier.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddCashierService(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        var connectionString = config.GetConnectionString("CashierConnectionString");
        services.AddDbContext<CashierDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ServiceExtensions)));
        services.AddScoped<ICashierRepository, CashierRepository>();
        services.AddScoped<IReadOnlyCashierRepository, ReadOnlyCashierRepository>();

        logger.Information("Cashier service added");
        return services;
    }
}

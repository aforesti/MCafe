using MCafe.Admin.Infrastructure.Data;
using MCafe.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MCafe.Admin.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddAdminService(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger)
    {
        var connectionString = config.GetConnectionString("AdminConnectionString");
        services.AddDbContext<AdminDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ServiceExtensions)));

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IReadOnlyAdminRepository, ReadOnlyAdminRepository>();

        logger.Information("Admin service added");
        return services;
    }
}

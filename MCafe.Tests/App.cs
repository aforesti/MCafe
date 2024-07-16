using MCafe.Admin;
using MCafe.Admin.Infrastructure.Data;
using MCafe.Cashier;
using MCafe.Cashier.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class App : AppFixture<Program>
{
    internal IAdminRepository AdminRepository => Services.GetService<IAdminRepository>() ?? throw new NullReferenceException("AdminRepository is null");

    internal ICashierRepository CashierRepository => Services.GetService<ICashierRepository>() ?? throw new NullReferenceException("CashierRepository is null");

    protected override async Task SetupAsync()
    {
        using var scope = Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AdminDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var cashierContext = services.GetRequiredService<CashierDbContext>();
        await cashierContext.Database.EnsureDeletedAsync();
        await cashierContext.Database.EnsureCreatedAsync();
    }
}

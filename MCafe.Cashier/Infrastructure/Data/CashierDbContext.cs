using MCafe.Cashier.Domain;
using Microsoft.EntityFrameworkCore;

namespace MCafe.Cashier.Infrastructure.Data;

public class CashierDbContext(DbContextOptions<CashierDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("cashier");
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Customer)
                .HasConversion(
                    customer => customer == null ? null : customer.Name,
                    name => name == null ? null : new Customer(name))
                .HasMaxLength(40);

            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.CreatedBy).IsRequired().HasMaxLength(40);
            entity.HasMany(e => e.Items);
            entity.Navigation(e => e.Items).AutoInclude();
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductId).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Price).IsRequired();
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }
}

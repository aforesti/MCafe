using MCafe.Barista.Domain;
using Microsoft.EntityFrameworkCore;

namespace MCafe.Barista.Infrastructure.Data;

public class BaristaDbContext(DbContextOptions<BaristaDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("barista");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.OrderPlacedAt).IsRequired();
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Quantity).IsRequired();
        });
    }
}

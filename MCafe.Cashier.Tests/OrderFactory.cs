using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Tests;

public static class OrderFactory
{
    public static Order CreateOrder(TimeProvider? timeProvider = null)
    {
        var createdAt = timeProvider?.GetUtcNow() ?? DateTimeOffset.UtcNow;
        return new Order("Test", createdAt);
    }

    public static Order WithItem(this Order order, OrderItem item)
    {
        order.AddItem(item);
        return order;
    }
}

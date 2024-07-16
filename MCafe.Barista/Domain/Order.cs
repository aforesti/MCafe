using MCafe.Shared.Interfaces;

namespace MCafe.Barista.Domain;

public class Order(
    Guid id,
    string customerName,
    DateTimeOffset orderPlacedAt) : IEntity
{
    public Guid Id { get; } = id;
    public string CustomerName { get; } = customerName;
    public OrderItem[] Items { get; } = [];
    public DateTimeOffset OrderPlacedAt { get; } = orderPlacedAt;

    public bool IsCompleted { get; private set; }
    public DateTimeOffset? OrderPreparedAt { get; private set; }

    public void CompleteOrder(DateTimeOffset preparedAt)
    {
        if (IsCompleted)
        {
            return;
        }

        IsCompleted = true;
        OrderPreparedAt = preparedAt;
    }
}

public record OrderItem
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; }
    public int Quantity { get; init; }

    public OrderItem(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}

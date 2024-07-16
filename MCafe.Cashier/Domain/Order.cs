using Ardalis.GuardClauses;
using MCafe.Shared.Interfaces;

namespace MCafe.Cashier.Domain;

public class Order(string createdBy, DateTimeOffset createdAt) : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Customer? Customer { get; private set; }

    public DateTimeOffset CreatedAt { get; } = createdAt;

    public string CreatedBy { get; } = Guard.Against.NullOrEmpty(createdBy);

    public DateTimeOffset? OrderPlacedAt { get; private set; }

    public void AddItem(OrderItem item)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(item.Quantity);
            return;
        }
        _items.Add(item);
    }

    public void RemoveItem(Guid productId, int quantity)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return;
        if (item.Quantity > quantity)
        {
            item.DecreaseQuantity(quantity);
            return;
        }

        _items.Remove(item);
    }

    public decimal Total => _items.Sum(x => x.Total);

    public void PlaceOrder(Customer customer, DateTimeOffset placedAt)
    {
        Customer = customer;
        OrderPlacedAt = placedAt;
    }
}

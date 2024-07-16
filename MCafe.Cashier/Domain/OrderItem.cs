using Ardalis.GuardClauses;
using MCafe.Shared.Interfaces;

namespace MCafe.Cashier.Domain;

public record OrderItem : IEntity
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; private set; }
    public decimal Price { get; }

    public OrderItem(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.Negative(price);
    }

    public void IncreaseQuantity(int quantity) => Quantity += quantity;

    public void DecreaseQuantity(int quantity) => Quantity -= quantity;

    public decimal Total => Quantity * Price;
}

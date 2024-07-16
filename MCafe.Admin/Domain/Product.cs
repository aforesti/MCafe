using Ardalis.GuardClauses;
using MCafe.Shared.Interfaces;

namespace MCafe.Admin.Domain;

public class Product(string name, decimal price) : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = Guard.Against.LengthOutOfRange(name, 3, 40);
    public decimal Price { get; private set; } = Guard.Against.Negative(price);
}

using Ardalis.GuardClauses;

namespace MCafe.Cashier.Domain;

public record Customer(string Name)
{
    public static implicit operator string(Customer customer) => customer.Name;
    public static implicit operator Customer(string name) => new(Guard.Against.LengthOutOfRange(name, 3, 50));
};

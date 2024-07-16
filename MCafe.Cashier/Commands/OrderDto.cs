namespace MCafe.Cashier.Commands;

public record OrderDto(
    Guid Id,
    string CreatedBy,
    DateTimeOffset CreatedAt,
    OrderItemDto[] Items,
    decimal Total
);

public record OrderItemDto(
    Guid ProductId,
    decimal Price,
    int Quantity);

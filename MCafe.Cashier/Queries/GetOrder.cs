using MCafe.Cashier.Commands;
using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Queries;

public record GetOrderById(Guid OrderId) : IRequest<OrderDto?>;

internal class GetOrderByIdHandler(IReadOnlyCashierRepository repository) : IRequestHandler<GetOrderById, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        var order = await repository.GetById<Order>(request.OrderId, cancellationToken);

        return order is null
            ? null
            : new OrderDto(
                order.Id,
                order.CreatedBy,
                order.CreatedAt,
                order.Items.Select(i => new OrderItemDto(
                    i.ProductId,
                    i.Price,
                    i.Quantity
                )).ToArray(),
                order.Total);
    }
}

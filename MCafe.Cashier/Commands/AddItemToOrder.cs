using ErrorOr;
using MCafe.Admin.Contracts;
using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Commands;

public record AddItemToOrder(Guid OrderId, Guid ProductId, int Quantity) : IRequest<ErrorOr<OrderItemDto>>;

internal sealed class AddItemToOrderHandler(
    ICashierRepository repository,
    IMediator mediator
    ) : IRequestHandler<AddItemToOrder, ErrorOr<OrderItemDto>>
{
    public async Task<ErrorOr<OrderItemDto>> Handle(AddItemToOrder command, CancellationToken cancellationToken)
    {
        var order = await repository.GetById<Order>(command.OrderId, cancellationToken);
        if (order is null)
        {
            return Error.NotFound(description: $"Order with id {command.OrderId} not found.");
        }

        var product = await mediator.Send(new GetProductById(command.ProductId), cancellationToken);
        if (product is null)
        {
            return Error.Validation(description: $"Product with id {command.ProductId} not found.");
        }

        var orderItem = new OrderItem(command.ProductId, command.Quantity, product.Price);
        repository.Add(orderItem);
        order.AddItem(orderItem);

        await repository.SaveChanges(cancellationToken);

        return new OrderItemDto(
            orderItem.ProductId,
            orderItem.Price,
            orderItem.Quantity);
    }
}

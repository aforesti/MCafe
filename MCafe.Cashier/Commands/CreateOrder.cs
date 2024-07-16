using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Commands;

public record CreateOrder(string CreatedBy) : IRequest<OrderDto>;

internal sealed class CreateOrderHandler(
    ICashierRepository repository,
    TimeProvider timeProvider) : IRequestHandler<CreateOrder, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrder command, CancellationToken cancellationToken)
    {
        var order = new Order(command.CreatedBy, timeProvider.GetUtcNow());
        repository.Add(order);
        await repository.SaveChanges(cancellationToken);

        return new OrderDto(order.Id, order.CreatedBy, order.CreatedAt, [], order.Total);
    }
}

using ErrorOr;
using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Commands;

public record PlaceOrder(Guid OrderId, Customer Customer) : IRequest<ErrorOr<Success>>;

internal sealed class PlaceOrderHandler(
    ICashierRepository repository,
    TimeProvider timeProvider) : IRequestHandler<PlaceOrder, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(PlaceOrder command, CancellationToken cancellationToken)
    {
        var order = await repository.GetById<Order>(command.OrderId, cancellationToken);

        if (order is null)
        {
            return Error.NotFound($"Order with id {command.OrderId} not found.");
        }

        order.PlaceOrder(command.Customer, timeProvider.GetUtcNow());

        await repository.SaveChanges(cancellationToken);

        return Result.Success;
    }
}

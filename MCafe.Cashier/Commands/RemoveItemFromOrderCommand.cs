using Ardalis.GuardClauses;
using MCafe.Cashier.Domain;
using MCafe.Shared.Interfaces;

namespace MCafe.Cashier.Commands;

public record RemoveItemFromOrderCommand(Guid OrderId, Guid ProductId, int Quantity) : IRequest;

internal sealed class RemoveItemFromOrderCommandHandler(ICashierRepository repository) : IRequestHandler<RemoveItemFromOrderCommand>
{
    public async Task Handle(RemoveItemFromOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await repository.GetById<Order>(command.OrderId, cancellationToken)
                    ?? throw new NotFoundException(command.OrderId.ToString(), nameof(Order));

        order.RemoveItem(command.ProductId, command.Quantity);

        await repository.SaveChanges(cancellationToken);
    }
}

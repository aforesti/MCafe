using FastEndpoints;
using MCafe.Cashier.Commands;

namespace MCafe.Cashier.Endpoints;

public record RemoveItemFromOrderRequest(Guid OrderId, Guid ProductId, int Quantity = 1);

public class RemoveItemFromOrder(IMediator mediator) : Endpoint<RemoveItemFromOrderRequest>
{
    public override void Configure()
    {
        Delete("/cashier/orders/{OrderId}/items/{ProductId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveItemFromOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveItemFromOrderCommand(request.OrderId, request.ProductId, request.Quantity);
        await mediator.Send(command, cancellationToken);
        await SendOkAsync(cancellationToken);
    }
}

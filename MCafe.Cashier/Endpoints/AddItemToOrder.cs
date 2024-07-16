using ErrorOr;
using FastEndpoints;
using MCafe.Cashier.Commands;

namespace MCafe.Cashier.Endpoints;

public record AddItemToOrderRequest(Guid OrderId, Guid ProductId, int Quantity);

internal class AddItemToOrderEndpoint(IMediator mediator) : Endpoint<AddItemToOrderRequest>
{
    public override void Configure()
    {
        Post("/cashier/orders/{orderId}/items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddItemToOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new AddItemToOrder(request.OrderId, request.ProductId, request.Quantity);
        var result = await mediator.Send(command, cancellationToken);
        await result.SwitchAsync(
            async value => await SendOkAsync(value, cancellationToken),
            async error => await SendAsync(error, 400, cancellationToken)
        );
    }

}

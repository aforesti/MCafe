using FastEndpoints;
using MCafe.Cashier.Commands;

namespace MCafe.Cashier.Endpoints;

public record PlaceOrderRequest(Guid OrderId, string CustomerName);

public class PlaceOrderEndpoint(IMediator mediator) : Endpoint<PlaceOrderRequest>
{
    public override void Configure()
    {
        Post("/cashier/orders/{OrderId}/place");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        var command = new PlaceOrder(request.OrderId, request.CustomerName);
        await mediator.Send(command, cancellationToken);
        await SendOkAsync(cancellationToken);
    }
}

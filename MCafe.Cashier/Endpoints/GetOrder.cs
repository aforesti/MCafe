using FastEndpoints;
using MCafe.Cashier.Commands;
using MCafe.Cashier.Queries;

namespace MCafe.Cashier.Endpoints;

public record OrderByIdRequest(Guid Id);

public sealed class GetOrderByIdEndpoint(IMediator mediator) : Endpoint<OrderByIdRequest, OrderDto>
{
    public override void Configure()
    {
        Get("/cashier/orders/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await mediator.Send(new GetOrderById(request.Id), cancellationToken);

        if (order is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        await SendAsync(order, 200, cancellationToken);
    }
}

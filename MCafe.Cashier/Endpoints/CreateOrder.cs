using FastEndpoints;
using MCafe.Cashier.Commands;

namespace MCafe.Cashier.Endpoints;

public record CreateOrderRequest(string CreatedBy);

internal class CreateOrderEndpoint(IMediator mediator) : Endpoint<CreateOrderRequest, OrderDto>
{
    public override void Configure()
    {
        Post("/cashier/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await mediator.Send(new CreateOrder(request.CreatedBy), cancellationToken);

        await SendCreatedAtAsync<GetOrderByIdEndpoint>(
            new { id = order.Id },
            order,
            cancellation: cancellationToken);
    }
}

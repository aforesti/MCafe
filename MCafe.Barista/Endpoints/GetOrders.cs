using FastEndpoints;
using MCafe.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order = MCafe.Barista.Domain.Order;

namespace MCafe.Barista.Endpoints;

public sealed class GetBaristaOrdersEndpoint(IReadOnlyRepository readRepository) : EndpointWithoutRequest<OrderDto[]>
{
    public override void Configure()
    {
        Get("/barista/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var orders = await readRepository.Items<Order>()
            .Select(x => new OrderDto(x.Id, x.CustomerName, x.Items.Select(i => new OrderItemDto(i.Name, i.Quantity)).ToArray(), x.OrderPlacedAt))
            .ToArrayAsync(ct);

        await SendAsync(orders, 200, ct);
    }
}

public record OrderDto(
    Guid Id,
    string CustomerName,
    OrderItemDto[] Items,
    DateTimeOffset OrderPlacedAt);

public record OrderItemDto(string Name, int Quantity);

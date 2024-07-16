using MCafe.Admin.Contracts;

namespace MCafe.Admin.Endpoints;

internal sealed class GetProductsEndpoint(IMediator mediator) : EndpointWithoutRequest<ProductDto[]>
{
    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var products = await mediator.Send(new GetProducts(), ct);
        await SendAsync(products, 200, ct);
    }
}

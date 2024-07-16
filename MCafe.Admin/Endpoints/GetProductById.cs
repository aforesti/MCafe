using MCafe.Admin.Contracts;

namespace MCafe.Admin.Endpoints;

public record GetProductByIdRequest(Guid Id);

internal sealed class GetProductByIdEndpoint(IMediator mediator) : Endpoint<GetProductByIdRequest, ProductDto>
{
    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductByIdRequest request, CancellationToken ct)
    {
        var product = await mediator.Send(new GetProductById(request.Id), ct);

        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendAsync(product, 200, ct);
    }
}

using MCafe.Admin.Contracts;

namespace MCafe.Admin.Endpoints;

public record CreateProductRequest(string Name, decimal Price);

internal sealed class CreateProductEndpoint(IAdminRepository repository)
    : Endpoint<CreateProductRequest, ProductDto, ProductMapper>
{
    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = repository.Add(Map.ToEntity(request));
        await repository.SaveChanges(cancellationToken);

        await SendCreatedAtAsync<GetProductByIdEndpoint>(
            new { id = product.Id },
            Map.FromEntity(product),
            cancellation: cancellationToken);
    }
}

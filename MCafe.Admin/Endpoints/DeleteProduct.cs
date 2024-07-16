using MCafe.Admin.Domain;

namespace MCafe.Admin.Endpoints;

public record DeleteProductRequest(Guid Id);

internal sealed class DeleteProductEndpoint(
    IAdminRepository repository)
    : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await repository.GetById<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        repository.Remove(product);
        await repository.SaveChanges(cancellationToken);

        await SendNoContentAsync(cancellationToken);
    }
}

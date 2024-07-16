using MCafe.Admin.Contracts;
using MCafe.Admin.Domain;

namespace MCafe.Admin.Queries;

internal sealed class GetProductByIdHandler(IReadOnlyAdminRepository repository) : IRequestHandler<GetProductById, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductById query, CancellationToken cancellation)
    {
        var product = await repository.GetById<Product>(query.Id, cancellation);

        return product is null ? null : new ProductDto(product.Id, product.Name, product.Price);
    }
}

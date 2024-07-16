using MCafe.Admin.Contracts;
using MCafe.Admin.Domain;
using Microsoft.EntityFrameworkCore;

namespace MCafe.Admin.Queries;

internal sealed class GetProductsHandler(IReadOnlyAdminRepository repository) : IRequestHandler<GetProducts, ProductDto[]>
{
    public async Task<ProductDto[]> Handle(GetProducts query, CancellationToken cancellation)
    {
        return await repository.Items<Product>()
            .Select(p => new ProductDto(p.Id, p.Name, p.Price))
            .ToArrayAsync(cancellation);
    }
}

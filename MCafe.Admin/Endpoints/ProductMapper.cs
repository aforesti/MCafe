using MCafe.Admin.Contracts;
using MCafe.Admin.Domain;

namespace MCafe.Admin.Endpoints;

internal class ProductMapper : Mapper<CreateProductRequest, ProductDto, Product>
{
    public override Product ToEntity(CreateProductRequest r) => new(r.Name, r.Price);

    public override ProductDto FromEntity(Product e) => new(e.Id, e.Name, e.Price);
}

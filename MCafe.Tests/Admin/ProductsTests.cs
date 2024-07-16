using MCafe.Admin.Contracts;
using MCafe.Admin.Domain;
using MCafe.Admin.Endpoints;

namespace Tests.Admin;

public class ProductsTests(App app) : TestBase<App>
{
    [Fact]
    public async Task CreateProduct_ShouldCreateAProduct()
    {
        var createRequest = new CreateProductRequest("Product 1", 10.0m);
        var (rsp, res) = await app.Client
            .POSTAsync<CreateProductEndpoint, CreateProductRequest, ProductDto>(createRequest);

        rsp.StatusCode.Should().Be(HttpStatusCode.Created);
        res.Name.Should().Be(createRequest.Name);
        res.Price.Should().Be(createRequest.Price);
        res.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts()
    {
        var (rsp, res) = await app.Client
            .GETAsync<GetProductsEndpoint, ProductDto[]>();

        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Should().NotBeEmpty();
    }


    [Fact]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        var product = new Product("Test", 0);
        app.AdminRepository.Add(product);
        await app.AdminRepository.SaveChanges();

        var (rsp, res) = await app.Client
            .GETAsync<GetProductByIdEndpoint, GetProductByIdRequest, ProductDto>(
                new GetProductByIdRequest(product.Id));

        rsp.EnsureSuccessStatusCode();
        res.Id.Should().Be(product.Id);
        res.Name.Should().Be(product.Name);
        res.Price.Should().Be(product.Price);
    }
}

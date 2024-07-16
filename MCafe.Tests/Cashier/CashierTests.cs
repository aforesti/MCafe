using MCafe.Cashier.Commands;
using MCafe.Cashier.Endpoints;
using Order = MCafe.Cashier.Domain.Order;

namespace Tests.Cashier;

public class CashierTests(App app) : TestBase<App>
{
    [Fact]
    public async Task CreateOrder_ShouldCreateAnOrder()
    {
        var createRequest = new CreateOrderRequest("Created-By-Test");
        var (rsp, res) = await app.Client
            .POSTAsync<CreateOrderEndpoint, CreateOrderRequest, OrderDto>(createRequest);

        rsp.StatusCode.Should().Be(HttpStatusCode.Created);
        res.CreatedBy.Should().Be(createRequest.CreatedBy);
        res.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        res.Items.Should().BeEmpty();
        res.Total.Should().Be(0);
        res.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnCorrectOrder()
    {
        var order = new Order("Test", DateTimeOffset.UtcNow);
        app.CashierRepository.Add(order);
        await app.CashierRepository.SaveChanges();

        var (rsp, res) = await app.Client
            .GETAsync<GetOrderByIdEndpoint, OrderByIdRequest, OrderDto>(
                new OrderByIdRequest(order.Id));

        rsp.EnsureSuccessStatusCode();
        res.Id.Should().Be(order.Id);
        res.CreatedBy.Should().Be(order.CreatedBy);
        res.CreatedAt.Should().Be(order.CreatedAt);
        res.Items.Should().BeEmpty();
        res.Total.Should().Be(0);
    }
}

using FluentAssertions;
using MCafe.Cashier.Domain;

namespace MCafe.Cashier.Tests;

public class OrderTests
{
    [Fact]
    public void WhenAddingProductToOrder_ShouldUpdateQtyOfExistingProduct()
    {
        // Arrange
        var orderItem = new OrderItem(Guid.NewGuid(), quantity: 1, price: 1);
        var order = OrderFactory.CreateOrder().WithItem(orderItem);

        // Act
        order.AddItem(orderItem);

        // Assert
        order.Items.Should().BeEquivalentTo([new { orderItem.ProductId, Quantity = 2 }]);
    }

    [Fact]
    public void WhenRemovingProductFromOrder_ShouldUpdateQtyOfExistingProduct()
    {
        // Arrange
        var orderItem = new OrderItem(Guid.NewGuid(), quantity: 3, price: 1);
        var order = OrderFactory.CreateOrder().WithItem(orderItem);

        // Act
        order.RemoveItem(orderItem.ProductId, 1);

        // Assert
        order.Items.Should().BeEquivalentTo([new { orderItem.ProductId, Quantity = 2 }]);
    }
}

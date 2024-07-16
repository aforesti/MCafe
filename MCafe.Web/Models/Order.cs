using System.ComponentModel.DataAnnotations;

namespace MCafe.Web.Models;

public class Order
{
    public Guid Id { get; set; }
    [Required]
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; } = [];
    public decimal Total => Items.Sum(x => x.Total);
    public void Clear()
    {
        Items.Clear();
        Id = Guid.Empty;
        CustomerName = string.Empty;
    }
}

public class OrderItem
{
    public Guid ProductId { get; init; }
    public int Quantity { get; set; }
    public decimal Price { get; init; }
    public decimal Total => Quantity * Price;
}

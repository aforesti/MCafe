using System.ComponentModel.DataAnnotations;

namespace MCafe.Web.Models;

public class Product
{
    public Guid Id { get; set; }
    [MinLength(3), MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Range(0.01, 10000)]
    public decimal Price { get; set; }

    public override string ToString() => Name;
}

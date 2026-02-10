
namespace Ecommerce.Application.DTOs.Product;

public class ProductResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public string? Image { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}

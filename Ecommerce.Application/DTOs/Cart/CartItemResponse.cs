namespace Ecommerce.Application.DTOs.Cart;

public class CartItemResponse
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public string ProductTitle { get; set; } = null!;

    public decimal ProductPrice { get; set; }

    public string? ProductImage { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }
}

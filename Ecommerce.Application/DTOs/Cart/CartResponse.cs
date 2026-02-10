namespace Ecommerce.Application.DTOs.Cart;

public class CartResponse
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public List<CartItemResponse> Items { get; set; } = new();

    public int TotalItems { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

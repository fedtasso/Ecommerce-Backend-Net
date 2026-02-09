namespace Ecommerce.Domain.Entities;

public class CartItem
{
    public long Id { get; private set; }

    public Cart Cart { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    public long CartId { get; private set; }
    
    public long ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Subtotal { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    // Constructor de dominio

    private CartItem() { } // TODO estudiar, agregado para resolver error en migrations
    public CartItem(Cart cart, Product product, int quantity)
    {
        Cart = cart;
        Product = product;

        Quantity = quantity;
        UnitPrice = Product.Price;

        RecalculateSubtotal();

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Regla de negocio
    private void RecalculateSubtotal()
    {
        Subtotal = UnitPrice * Quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;
        RecalculateSubtotal();
    }

    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
        RecalculateSubtotal();
    }
}

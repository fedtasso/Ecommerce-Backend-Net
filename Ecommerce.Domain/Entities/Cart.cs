namespace Ecommerce.Domain.Entities;

public class Cart
{
    public long Id { get; private set; }

    public long UserId { get; private set; }

    public User User { get; private set; } = null!;

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice { get; private set; }

    public int TotalItems { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Constructor de dominio
    public Cart(User user)
    {
        User = user;

        TotalPrice = 0;
        TotalItems = 0;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Reglas de negocio
    private void RecalculateTotals()
    {
        TotalItems = _items.Sum(i => i.Quantity);
        TotalPrice = _items.Sum(i => i.Subtotal);

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(CartItem newItem)
    {
        var existingItem = _items
            .FirstOrDefault(i => i.Product.Id == newItem.Product.Id);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(newItem.Quantity);
        }
        else
        {
            _items.Add(newItem);
        }

        RecalculateTotals();
    }

    public void RemoveItem(CartItem item)
    {
        _items.Remove(item);
        RecalculateTotals();
    }

    public void Clear()
    {
        _items.Clear();
        RecalculateTotals();
    }
}

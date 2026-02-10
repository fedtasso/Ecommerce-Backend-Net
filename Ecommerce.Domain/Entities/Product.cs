namespace Ecommerce.Domain.Entities;

public class Product
{
    public long Id { get; private set; }

    public string Title { get; private set; } = null!;

    public decimal Price { get; private set; }

    public string? Description { get; private set; }

    public string Category { get; private set; } = null!; // TODO pasar a enum

    public string? Image { get; private set; }

    public int Stock { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Constructor de dominio
    public Product(
        string title,
        decimal price,
        string category,
        int stock,
        string? description = null,
        string? image = null
    )
    {
        Title = title;
        Price = price;
        Category = category;
        Stock = stock;
        Description = description;
        Image = image;

        CreatedAt = DateTime.UtcNow;
    }

    // Reglas de negocio

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("La cantidad debe ser positiva");

        if (quantity > Stock)
            throw new InvalidOperationException("Stock Insuficiente");

        Stock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("La cantidad debe ser positiva");

        Stock += quantity;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("El precio debe ser positivo");

        Price = newPrice;
    }

      public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("El título no puede estar vacío");
        Title = title;
    }

    public void SetCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("La categoría no puede estar vacía");
        Category = category;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetImage(string? image)
    {
        Image = image;
    }

    public void SetStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentException("El stock no puede ser negativo");
        Stock = stock;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Product;

public class ProductCreateRequest
{
    [Required(ErrorMessage = "El título es obligatorio")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public string Category { get; set; } = null!;

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    public string? Description { get; set; }
    public string? Image { get; set; }
}

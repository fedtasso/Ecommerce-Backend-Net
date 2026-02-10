using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Product;

public class ProductUpdateRequest
{
    public string? Title { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal? Price { get; set; }

    public string? Category { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int? Stock { get; set; }

    public string? Description { get; set; }
    public string? Image { get; set; }
}

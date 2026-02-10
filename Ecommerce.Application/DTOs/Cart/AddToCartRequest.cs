using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Cart;

public class AddToCartRequest
{
    [Required(ErrorMessage = "El ID del producto es requerido")]
    public long ProductId { get; set; }

    [Required(ErrorMessage = "La cantidad es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
    public int Quantity { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Ecommerce.Shared.Constants;


namespace Ecommerce.Application.DTOs.User;

public class UserCreateRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email es inválido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [RegularExpression(
        ValidationConstants.PasswordRegex,
        ErrorMessage = ValidationConstants.PasswordMessage
    )]
    public string Password { get; set; } = null!;

    public string? Avatar { get; set; }

    [RegularExpression(
        ValidationConstants.PhoneRegex,
        ErrorMessage = "Formato de teléfono inválido"
    )]
    public string? Phone { get; set; }
}

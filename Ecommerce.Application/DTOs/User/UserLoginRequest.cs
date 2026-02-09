using System.ComponentModel.DataAnnotations;
using Ecommerce.Shared.Constants;

namespace Ecommerce.Application.DTOs.User;

public class UserLoginRequest
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [RegularExpression(
        ValidationConstants.PasswordRegex,
        ErrorMessage = ValidationConstants.PasswordMessage
    )]
    public string Password { get; set; } = null!;
}

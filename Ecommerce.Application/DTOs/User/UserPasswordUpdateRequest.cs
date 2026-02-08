using System.ComponentModel.DataAnnotations;
using Ecommerce.Shared.Constants;

namespace Ecommerce.Application.DTOs.Users;

public class UserPasswordUpdateRequest
{
    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    public string CurrentPassword { get; init; } = null!;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [RegularExpression(
        ValidationConstants.PasswordRegex,
        ErrorMessage = ValidationConstants.PasswordMessage
    )]
    public string NewPassword { get; init; } = null!;
}

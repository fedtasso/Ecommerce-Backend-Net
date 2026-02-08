using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Users;

public class UserUpdateRequest
{
    public string? Name { get; init; }

    [EmailAddress(ErrorMessage = "El formato del email es inválido")]
    public string? Email { get; init; }

    public string? Avatar { get; init; }

    [RegularExpression(
        @"^[+]?[(]?\d{1,4}[)]?[-.\s]*\d{1,4}[-.\s]*\d{1,4}[-.\s]*\d{1,9}$",
        ErrorMessage = "Formato de teléfono inválido"
    )]
    public string? Phone { get; init; }
}

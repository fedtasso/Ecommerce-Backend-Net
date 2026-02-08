using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.DTOs.Users;

public class UserResponse
{
    public long Id { get; init; }

    public string Name { get; init; } = default!;

    public string Email { get; init; } = default!;

    public UserRole Role { get; init; }

    public string? Avatar { get; init; }

    public bool Active { get; init; }

    public DateTime? LastLogin { get; init; }

    public string? Phone { get; init; }

    public DateTime CreatedAt { get; init; }
}

using Ecommerce.Domain.Enums;
namespace Ecommerce.Domain.Entities;

public class User
{
    public long Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;

    public UserRole Role { get; private set; }

    public string? Phone { get; private set; }
    public string? Avatar { get; private set; }

    public bool Active { get; private set; }

    public DateTime? LastLogin { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Constructor de dominio
    public User(
        string name,
        string email,
        string password,
        UserRole role = UserRole.Customer
    )
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;

        Active = true;
        CreatedAt = DateTime.UtcNow;
    }

    // Métodos de negocio (ejemplo)
    public void Deactivate()
    {
        Active = false;
    }

    public void RegisterLogin()
    {
        LastLogin = DateTime.UtcNow;
    }
}

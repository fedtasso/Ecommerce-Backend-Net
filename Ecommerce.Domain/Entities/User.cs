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

    public void SetAvatar(string? avatar)
    {
        Avatar = avatar;
    }

    public void SetPhone(string? phone)
    {
        Phone = phone;
    }

    public void SetPassword(string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentException("La contrasela es requerida");

        Password = hashedPassword;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es requerido");

        Name = name;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email es requerido");

        Email = email;
    }

}

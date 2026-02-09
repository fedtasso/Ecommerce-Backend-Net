namespace Ecommerce.Shared.Constants;

public static class ValidationConstants
{
    public const string PasswordRegex = "^(?=.*\\d)(?=.*[A-Z]).{8,}$";
    
    public const string PasswordMessage = "La contraseña debe tener mínimo 8 caracteres, al menos un número y una letra mayúscula";

    public const string PhoneRegex = "^[+]?[0-9\\s]{10,15}$";
}

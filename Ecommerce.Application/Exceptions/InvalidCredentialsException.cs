namespace Ecommerce.Application.Exceptions;

public class InvalidCredentialsException : BusinessException
{
    public InvalidCredentialsException()
        : base("Credenciales inválidas")
    {
    }
}

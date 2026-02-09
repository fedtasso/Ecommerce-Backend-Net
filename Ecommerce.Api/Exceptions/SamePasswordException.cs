namespace Ecommerce.Application.Exceptions;

public class SamePasswordException : BusinessException
{
    public SamePasswordException()
        : base("La nueva contraseña es igual a la actual")
    {
    }
}

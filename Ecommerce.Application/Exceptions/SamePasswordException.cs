namespace Ecommerce.Application.Exceptions;

public class SamePasswordException : BusinessException
{
    public SamePasswordException()
        : base("La nueva contraseña no puede ser igual a la actual")
    {
    }
}

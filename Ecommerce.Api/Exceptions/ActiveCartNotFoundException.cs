namespace Ecommerce.Application.Exceptions;

public class ActiveCartNotFoundException : BusinessException
{
    public ActiveCartNotFoundException(long userId)
        : base($"Carrito no encontrado para el usuario {userId}")
    {
    }
}

namespace Ecommerce.Application.Exceptions;

public class UserNotFoundException : BusinessException
{
    public UserNotFoundException(long userId)
        : base($"El usuario id número {userId} no fue encontrado")
    {
    }

    public UserNotFoundException(string email)
        : base($"El email {email} no fue encontrado")
    {
    }
}

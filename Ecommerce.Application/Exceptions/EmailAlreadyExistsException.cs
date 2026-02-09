namespace Ecommerce.Application.Exceptions;

public class EmailAlreadyExistsException : BusinessException
{
    public EmailAlreadyExistsException(string email)
        : base($"El email {email} ya se encuentra registrado")
    {
    }
}

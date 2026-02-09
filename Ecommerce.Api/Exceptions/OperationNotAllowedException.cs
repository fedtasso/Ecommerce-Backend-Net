namespace Ecommerce.Application.Exceptions;

public class OperationNotAllowedException : BusinessException
{
    public OperationNotAllowedException(string reason)
        : base(reason)
    {
    }
}

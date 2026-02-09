namespace Ecommerce.Application.Exceptions;

public class ProductNotFoundException : BusinessException
{
    public ProductNotFoundException(long productId)
        : base($"El producto id número {productId} no fue encontrado")
    {
    }
}

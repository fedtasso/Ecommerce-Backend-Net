namespace Ecommerce.Application.Exceptions;

public class ProductNotInCartException : BusinessException
{
    public ProductNotInCartException(long productId)
        : base($"El producto id número {productId} no está en el carrito")
    {
    }
}

namespace Ecommerce.Application.Exceptions;

public class InsufficientStockException : BusinessException
{
    public InsufficientStockException(string productTitle, int requestedQuantity, int availableStock)
        : base(
            $"Stock insuficiente para el producto '{productTitle}'. " +
            $"Solicitado: {requestedQuantity}, Disponible: {availableStock}"
        )
    {
    }
}

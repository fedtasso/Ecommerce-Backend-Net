using Ecommerce.Application.DTOs.Product;

namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(ProductCreateRequest request);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse> GetProductByIdAsync(long id);
    Task<ProductResponse> UpdateProductAsync(long id, ProductUpdateRequest request);
    Task DeleteProductAsync(long id);
}

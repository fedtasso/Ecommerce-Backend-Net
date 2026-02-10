using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Exceptions;

namespace Ecommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ProductMapper _productMapper;

    public ProductService(IProductRepository productRepository, ProductMapper productMapper)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
    }


    public async Task<ProductResponse> CreateProductAsync(ProductCreateRequest request)
    {
        var product = _productMapper.ToEntity(request);
    //TODO verificar si el producto ya existe

        await _productRepository.AddAsync(product);
        return _productMapper.ToResponse(product);
    }


    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => _productMapper.ToResponse(p));
    }


    public async Task<ProductResponse> GetProductByIdAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            throw new ProductNotFoundException(id);

        return _productMapper.ToResponse(product);
    }


    public async Task<ProductResponse> UpdateProductAsync(long id, ProductUpdateRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            throw new ProductNotFoundException(id);

        _productMapper.UpdateEntity(product, request);

        await _productRepository.UpdateAsync(product);

        return _productMapper.ToResponse(product);
    }

    
    public async Task DeleteProductAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            throw new ProductNotFoundException(id);

        await _productRepository.DeleteAsync(product);
    }
}

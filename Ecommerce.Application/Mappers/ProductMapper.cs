using Ecommerce.Application.DTOs.Product;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public class ProductMapper
{
    public Product ToEntity(ProductCreateRequest request)
    {
        return new Product(
            title: request.Title,
            price: request.Price,
            category: request.Category,
            stock: request.Stock,
            description: request.Description,
            image: request.Image
        );
    }

    public ProductResponse ToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt
        };
    }

    public void UpdateEntity(Product product, ProductUpdateRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Title))
            product.SetTitle(request.Title);

        if (request.Price.HasValue)
            product.UpdatePrice(request.Price.Value);

        if (!string.IsNullOrWhiteSpace(request.Category))
            product.SetCategory(request.Category);

        if (!string.IsNullOrWhiteSpace(request.Description))
            product.SetDescription(request.Description);

        if (!string.IsNullOrWhiteSpace(request.Image))
            product.SetImage(request.Image);

        if (request.Stock.HasValue)
            product.SetStock(request.Stock.Value);
    }
}

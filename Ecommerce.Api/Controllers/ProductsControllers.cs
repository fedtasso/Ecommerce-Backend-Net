using Ecommerce.Application.DTOs.Product;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(
        [FromBody][Required] ProductCreateRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
        return Ok(response);
    }

    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
    {
        var response = await _productService.GetAllProductsAsync();
        return Ok(response);
    }

    // GET /api/products/{id}
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductResponse>> GetProductById([FromRoute] long id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return Ok(response);
    }

    // PUT /api/products/{id}
    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(
        [FromRoute] long id,
        [FromBody] ProductUpdateRequest request)
    {
        var response = await _productService.UpdateProductAsync(id, request);
        return Ok(response);
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] long id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}

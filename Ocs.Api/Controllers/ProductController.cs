using Microsoft.AspNetCore.Mvc;
using Ocs.Database.Repository.Interfaces;
using Ocs.Domain.Dto.Product;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository) => _productRepository = productRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productRepository.GetProductsAsync());

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDtoRequest productDto)
    {
        var product = await _productRepository.AddProductAsync(productDto);

        if (product == null)
        {
            return BadRequest("Товар уже существует");
        }

        return Ok(product);
    }
}
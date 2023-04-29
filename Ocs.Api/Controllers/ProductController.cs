using Microsoft.AspNetCore.Mvc;
using Ocs.Database.Services.Interfaces;
using Ocs.Dto.Product;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productService.GetProductsAsync());

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductRequestDto productRequestDto)
    {
        var product = await _productService.AddProductAsync(productRequestDto);

        if (product == null)
            return BadRequest("Товар уже существует");

        return Ok(product);
    }
}
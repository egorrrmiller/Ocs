using Microsoft.AspNetCore.Mvc;
using Ocs.Database.Repository.Interfaces;
using Ocs.Domain.Dto;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _productRepository.GetProductsAsync());
    
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto productDto) => Ok(await _productRepository.AddProductAsync(productDto));  
}
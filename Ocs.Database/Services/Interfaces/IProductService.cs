using Ocs.Domain.Models;
using Ocs.Dto.Product;

namespace Ocs.Database.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();

    Task<Product?> AddProductAsync(ProductRequestDto productRequestDto);
}
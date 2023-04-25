using Ocs.Domain.Dto;
using Ocs.Domain.Models;

namespace Ocs.Database.Repository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> AddProductAsync(ProductDto productDto);
}
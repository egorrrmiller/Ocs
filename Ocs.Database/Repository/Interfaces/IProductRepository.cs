using Ocs.Domain.Dto.Product;
using Ocs.Domain.Models;

namespace Ocs.Database.Repository.Interfaces;

public interface IProductRepository
{
	Task<List<Product>> GetProductsAsync();

	Task<Product?> AddProductAsync(ProductDtoRequest productDto);
}
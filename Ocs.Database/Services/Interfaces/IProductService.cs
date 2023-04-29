using Ocs.Domain.Dto.Product;
using Ocs.Domain.Models;

namespace Ocs.Database.Services.Interfaces;

public interface IProductService
{
	Task<List<Product>> GetProductsAsync();

	Task<Product?> AddProductAsync(ProductDtoRequest productDto);
}
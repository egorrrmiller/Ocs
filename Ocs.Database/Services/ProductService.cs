using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Dto.Product;
using Ocs.Domain.Models;

namespace Ocs.Database.Services;

public class ProductService : IProductService
{
	private readonly OcsContext _context;

	public ProductService(OcsContext context) => _context = context;

	public async Task<List<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

	public async Task<Product?> AddProductAsync(ProductDtoRequest productDto)
	{
		var productExists = await _context.Products.FirstOrDefaultAsync(id => id.Id == productDto.Id);

		if (productExists == null)
		{
			return null;
		}

		var product = await _context.Products.AddAsync(new()
		{
			Id = productDto.Id
		});

		await _context.SaveChangesAsync();

		return product.Entity;
	}
}
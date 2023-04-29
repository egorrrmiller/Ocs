using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Models;

namespace Ocs.Database.Services;

public class ProductService : IProductService
{
    private readonly OcsContext _context;

    public ProductService(OcsContext context) => _context = context;

    public async Task<List<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

    public async Task<Product?> AddProductAsync(Product product)
    {
        var productExists = await _context.Products.FirstOrDefaultAsync(id => id.Id == product.Id);

        if (productExists == null)
            return null;

        var productUpdate = await _context.Products.AddAsync(new()
        {
            Id = product.Id
        });

        await _context.SaveChangesAsync();

        return productUpdate.Entity;
    }
}
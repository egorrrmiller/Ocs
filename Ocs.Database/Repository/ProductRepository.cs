using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Repository.Interfaces;
using Ocs.Domain.Dto;
using Ocs.Domain.Models;

namespace Ocs.Database.Repository;

public class ProductRepository : IProductRepository
{
    private readonly OcsContext _context;

    public ProductRepository(OcsContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        var product = await _context.Products.AddAsync(new Product()
        {
            Id = productDto.Id,
            Qty = productDto.Qty
        });

        await _context.SaveChangesAsync();

        return product.Entity;
    }
}
using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Models;

namespace Ocs.Database.Services;

public class OrderService : IOrderService
{
    private readonly OcsContext _context;

    public OrderService(OcsContext context) => _context = context;

    public async Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.OrderLines)
            .FirstOrDefaultAsync(orderId => orderId.Id == id && orderId.Deleted == false, cancellationToken);

        return order ?? null;
    }

    public async Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        order.Created = DateTime.UtcNow;

        var orderUpdate = await _context.Orders.AddAsync(order,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    public async Task<Order?> UpdateOrderAsync(Order order, ICollection<OrderLines>? orderLines,
                                               CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.OrderLines.RemoveRange(orderLines);

        var orderUpdate = _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    public async Task<bool> DeleteOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
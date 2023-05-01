using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Enums;
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

        var idExist = await _context.Orders.FirstOrDefaultAsync(id => id.Id == order.Id, cancellationToken);

        if (idExist != null)
            throw new ArgumentException($"Id {order.Id} зарезервирован. Сгенерируйте новый.");

        var product = await _context.Line.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var line in order.OrderLines)
        {
            if (!product.Exists(x => x.Id == line.LineId))
                throw new ArgumentException($"Товара с Id: {line.LineId} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть меньше 1");
        }

        order.Status = OrderStatus.New;

        var orderUpdate = await _context.Orders.AddAsync(order,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    public async Task<Order?> UpdateOrderAsync(Guid id, Order order,
                                               CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var orderContext = await _context.Orders
            .Include(lines => lines.OrderLines)
            .FirstOrDefaultAsync(order => order.Id == id && order.Deleted == false, cancellationToken);

        var product = await _context.Line.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        if (orderContext == null)
            return null;

        if (orderContext.Status is OrderStatus.Paid or OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
            throw new ArgumentException("Заказы в статусах оплачен, передан в доставку, доставлен, завершен нельзя редактировать");

        foreach (var line in order.OrderLines)
        {
            if (!product.Exists(x => x.Id == line.LineId))
                throw new ArgumentException($"Товара с Id: {line.LineId} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть меньше 1");
        }

        _context.OrderLines.RemoveRange(orderContext.OrderLines);

        orderContext.Status = order.Status;
        orderContext.OrderLines = order.OrderLines;

        var orderUpdate = _context.Orders.Update(orderContext);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _context.Orders.AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id && order.Deleted == false, cancellationToken);

        if (order == null)
            return false;

        order.Deleted = order.Status is OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed
            ? throw new ArgumentException("Заказы в статусах передан в доставку, доставлен, завершен нельзя удалить")
            : true;

        _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
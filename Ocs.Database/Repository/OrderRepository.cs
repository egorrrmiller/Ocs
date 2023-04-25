using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Repository.Interfaces;
using Ocs.Domain.Dto;
using Ocs.Domain.Enums;

namespace Ocs.Database.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OcsContext _context;

    public OrderRepository(OcsContext context) => _context = context;

    public async Task<OrderDtoResponse?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.Lines)
            .FirstOrDefaultAsync(orderId => orderId.Id == id, cancellationToken);

        if (order == null || order.Deleted)
        {
            return null;
        }

        return new(order.Id, order.Status.ToString(), order.Created.ToString("yyyy-MM-dd hh:mm.s"),
            order.Lines);
    }

    public async Task<OrderDtoResponse> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (orderDto.Lines.Any(count => count.Qty < 1))
        {
            throw new ArgumentException("Количество товаров не должно быть меньше одного");
        }

        var order = await _context.Orders.AddAsync(new()
        {
            Id = orderDto.Id,
            Status = OrderStatus.New,
            Lines = orderDto.Lines
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(order.Entity.Id, order.Entity.Status.ToString(), order.Entity.Created.ToString("yyyy-MM-dd hh:mm.s"),
            order.Entity.Lines);
    }

    public async Task<OrderDtoResponse?> UpdateOrderAsync(Guid id, OrderDtoUpdate orderDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.Lines)
            .FirstOrDefaultAsync(orderId => orderId.Id == id, cancellationToken);

        if (order == null || order.Deleted)
        {
            return null;
        }

        if (order.Status is OrderStatus.Paid or OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
        {
            throw new ArgumentException("Заказы в статусах “оплачен”, “передан в доставку”, “доставлен”, “завершен” нельзя редактировать");
        }

        if (orderDto.Lines.Any(count => count.Qty < 1))
        {
            throw new ArgumentException("Количество товаров не должно быть меньше одного");
        }

        order.Status = Enum.Parse<OrderStatus>(orderDto.Status);
        var orderUpdate = _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return new(orderUpdate.Entity.Id, orderUpdate.Entity.Status.ToString(), orderUpdate.Entity.Created.ToString("yyyy-MM-dd hh:mm.s"),
            orderUpdate.Entity.Lines);
    }

    public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.Lines)
            .FirstOrDefaultAsync(orderId => orderId.Id == id, cancellationToken);

        if (order == null || order.Deleted)
        {
            return false;
        }

        if (order.Status is OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
        {
            throw new ArgumentException("Заказы в статусах “передан в доставку”, “доставлен”, “завершен” нельзя удалить");
        }

        order.Deleted = true;
        _context.Orders.Update(order);
        _context.Products.RemoveRange(order.Lines);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
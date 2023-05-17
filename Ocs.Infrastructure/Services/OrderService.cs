using Microsoft.EntityFrameworkCore;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;
using Ocs.Infrastructure.Context;
using Ocs.Infrastructure.Services.Interfaces;

namespace Ocs.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly OcsContext _context;

    public OrderService(OcsContext context) => _context = context;

    /// <summary>
    /// Получения заказа по Id
    /// </summary>
    /// <param name="id"> Id заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Заказ с заданным id </returns>
    public async Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.OrderLines)
            .FirstOrDefaultAsync(orderId => orderId.Id == id && orderId.Deleted == false, cancellationToken);

        return order ?? null;
    }

    /// <summary>
    /// Добавление нового заказа
    /// </summary>
    /// <param name="order"> Тело заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Созданный заказ </returns>
    public async Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        order.Status = OrderStatus.New;
        order.Created = DateTime.UtcNow;

        var orderUpdate = await _context.Orders.AddAsync(order,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    /// <summary>
    /// Обновление/редактирование заказа
    /// </summary>
    /// <param name="order"> Тело заказа </param>
    /// <param name="orderLines"> Строки старого заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Обновленный заказ </returns>
    public async Task<Order?> UpdateOrderAsync(Order order, ICollection<OrderLines>? orderLines,
                                               CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.OrderLines.RemoveRange(orderLines);

        var orderUpdate = _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return orderUpdate.Entity;
    }

    /// <summary>
    /// Удаление заказа
    /// </summary>
    /// <param name="order"> Тело заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> true если удален, false если не удален </returns>
    public async Task<bool> DeleteOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
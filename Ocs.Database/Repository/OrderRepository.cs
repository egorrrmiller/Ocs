using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Repository.Interfaces;
using Ocs.Domain.Dto.Order;
using Ocs.Domain.Dto.Product;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;

namespace Ocs.Database.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OcsContext _context;

    public OrderRepository(OcsContext context) => _context = context;

    public async Task<OrderDtoResponse?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.OrderProducts)
            .FirstOrDefaultAsync(orderId => orderId.Id == id, cancellationToken);

        if (order == null || order.Deleted)
        {
            return null;
        }

        var orderLines = order.OrderProducts.Select(lines => new ProductDtoResponse(lines.ProductId, lines.Qty))
            .ToList();

        return new(order.Id, order.Status.ToString(), order.Created.ToString("yyyy-MM-dd hh:mm.s"),
            orderLines);
    }

    public async Task<OrderDtoResponse> AddOrderAsync(OrderDtoRequest orderDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = await _context.Products.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        orderDto.Lines.Any(productId =>
        {
            if (!product.Exists(x => x.Id == productId.Id))
            {
                throw new ArgumentException($"Товара с Id: {productId} не существует");
            }

            return true;
        });

        if (orderDto.Lines.Any(count => count.Qty < 1))
        {
            throw new ArgumentException("Количество товаров не должно быть меньше одного");
        }

        var order = await _context.Orders.AddAsync(new()
        {
            Id = orderDto.Id,
            Status = OrderStatus.New
        }, cancellationToken);

        await _context.OrderProducts.AddRangeAsync(orderDto.Lines.Select(productId => new OrderProduct
        {
            OrderId = orderDto.Id,
            ProductId = productId.Id,
            Qty = productId.Qty
        }), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(order.Entity.Id, order.Entity.Status.ToString(), order.Entity.Created.ToString("yyyy-MM-dd hh:mm.s"),
            orderDto.Lines);
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

        var orderStatus = Enum.Parse<OrderStatus>(orderDto.Status);
        var orderUpdate = _context.Orders.Update(new()
        {
            Id = order.Id,
            Status = orderStatus,
            Created = order.Created
        });

        await _context.SaveChangesAsync(cancellationToken);

        return new(orderUpdate.Entity.Id, orderUpdate.Entity.Status.ToString(), orderUpdate.Entity.Created.ToString("yyyy-MM-dd hh:mm.s"),
            orderDto.Lines);
    }

    public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _context.Orders.AsNoTracking()
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

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
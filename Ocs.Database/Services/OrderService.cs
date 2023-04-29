using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;
using Ocs.Dto.Order;
using Ocs.Dto.Product;

namespace Ocs.Database.Services;

public class OrderService : IOrderService
{
    private readonly OcsContext _context;

    public OrderService(OcsContext context) => _context = context;

    public async Task<OrderResponseDto?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.AsNoTracking()
            .Include(lines => lines.OrderProducts)
            .FirstOrDefaultAsync(orderId => orderId.Id == id && orderId.Deleted == false, cancellationToken);

        if (order == null)
            return null;

        var orderLines = order.OrderProducts?.Select(lines => new ProductResponseDto(lines.ProductId, lines.Qty))
            .ToList();

        return new(order.Id,
            order.Status.ToString(),
            order.Created.ToString("yyyy-MM-dd HH:mm.s"),
            orderLines);
    }

    public async Task<OrderResponseDto?> AddOrderAsync(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idExist = await _context.Orders.FirstOrDefaultAsync(id => id.Id == orderRequestDto.Id, cancellationToken);

        if (idExist != null)
            throw new ArgumentException($"Id {orderRequestDto.Id} зарезервирован. Сгенерируйте новый.");

        var product = await _context.Products.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        orderRequestDto.Lines.ForEach(line =>
        {
            if (!product.Exists(x => x.Id == line.Id))
                throw new ArgumentException($"Товара с Id: {line.Id} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть < 1");
        });

        var order = await _context.Orders.AddAsync(new()
            {
                Id = orderRequestDto.Id,
                Status = OrderStatus.New,
                OrderProducts = orderRequestDto.Lines.Select(productId => new OrderProduct
                    {
                        OrderId = orderRequestDto.Id,
                        ProductId = productId.Id,
                        Qty = productId.Qty
                    })
                    .ToList()
            },
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(order.Entity.Id,
            order.Entity.Status.ToString(),
            order.Entity.Created.ToString("yyyy-MM-dd HH:mm.s"),
            orderRequestDto.Lines);
    }

    public async Task<OrderResponseDto?> UpdateOrderAsync(Guid id, OrderUpdateDto orderUpdateDto,
                                                          CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _context.Orders
            .Include(lines => lines.OrderProducts)
            .FirstOrDefaultAsync(order => order.Id == id && order.Deleted == false, cancellationToken);

        var product = await _context.Products.AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        if (order == null)
            return null;

        if (order.Status is OrderStatus.Paid or OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
            throw new ArgumentException("Заказы в статусах оплачен, передан в доставку, доставлен, завершен нельзя редактировать");

        orderUpdateDto.Lines.ForEach(line =>
        {
            if (!product.Exists(x => x.Id == line.Id))
                throw new ArgumentException($"Товара с Id: {line.Id} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть < 1");
        });

        var orderStatus = Enum.TryParse(orderUpdateDto.Status, out OrderStatus status)
            ? status
            : throw new ArgumentException("Неккоретный статус заказа.");

        _context.OrderProducts.RemoveRange(order.OrderProducts);

        order.Status = orderStatus;

        order.OrderProducts = orderUpdateDto.Lines.Select(product => new OrderProduct
            {
                OrderId = id,
                ProductId = product.Id,
                Qty = product.Qty
            })
            .ToList();

        var orderUpdate = _context.Orders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);

        return new(orderUpdate.Entity.Id,
            orderUpdate.Entity.Status.ToString(),
            orderUpdate.Entity.Created.ToString("yyyy-MM-dd HH:mm.s"),
            orderUpdateDto.Lines);
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
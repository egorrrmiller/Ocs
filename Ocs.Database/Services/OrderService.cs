using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Dto.Order;
using Ocs.Domain.Dto.Product;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;

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
		{
			return null;
		}

		var orderLines = order.OrderProducts?.Select(lines => new ProductResponseDto(lines.ProductId, lines.Qty))
			.ToList();

		return new(order.Id,
			order.Status.ToString(),
			order.Created.ToString("yyyy-MM-dd HH:mm.s"),
			orderLines);
	}

	public async Task<OrderResponseDto> AddOrderAsync(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();

		var product = await _context.Products.AsNoTracking()
			.ToListAsync(cancellationToken: cancellationToken);

		orderRequestDto.Lines.ForEach(line =>
		{
			if (!product.Exists(x => x.Id == line.Id))
			{
				throw new ArgumentException($"Товара с Id: {line.Id} не существует");
			}
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

		var order = await _context.Orders.AsNoTracking()
			.FirstOrDefaultAsync(order => order.Id == id && order.Deleted == false, cancellationToken);

		if (order == null)
		{
			return null;
		}

		var orderStatus = order.Status is OrderStatus.Paid or OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed
			? throw new ArgumentException("Заказы в статусах “оплачен”, “передан в доставку”, “доставлен”, “завершен” нельзя редактировать")
			: Enum.Parse<OrderStatus>(orderUpdateDto.Status);

		var orderUpdate = _context.Orders.Update(new()
		{
			Id = order.Id,
			Status = orderStatus,
			Created = order.Created
		});

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
		{
			return false;
		}

		order.Deleted = order.Status is OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed
			? throw new ArgumentException("Заказы в статусах “передан в доставку”, “доставлен”, “завершен” нельзя удалить")
			: true;

		_context.Orders.Update(order);

		await _context.SaveChangesAsync(cancellationToken);

		return true;
	}
}
using Ocs.Domain.Dto.Order;

namespace Ocs.Database.Services.Interfaces;

public interface IOrderService
{
	Task<OrderDtoResponse?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

	Task<OrderDtoResponse> AddOrderAsync(OrderDtoRequest orderDto, CancellationToken cancellationToken = default);

	Task<OrderDtoResponse?> UpdateOrderAsync(Guid id, OrderDtoUpdate orderDto, CancellationToken cancellationToken = default);

	Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
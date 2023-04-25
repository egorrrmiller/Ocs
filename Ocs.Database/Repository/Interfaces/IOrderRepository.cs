using Ocs.Domain.Dto;

namespace Ocs.Database.Repository.Interfaces;

public interface IOrderRepository
{
    Task<OrderDtoResponse?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<OrderDtoResponse> AddOrderAsync(OrderDto orderDto, CancellationToken cancellationToken = default);

    Task<OrderDtoResponse?> UpdateOrderAsync(Guid id, OrderDtoUpdate orderDto, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
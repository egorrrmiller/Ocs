using Ocs.Dto.Order;

namespace Ocs.Database.Services.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<OrderResponseDto?> AddOrderAsync(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default);

    Task<OrderResponseDto?> UpdateOrderAsync(Guid id, OrderUpdateDto orderUpdateDto, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
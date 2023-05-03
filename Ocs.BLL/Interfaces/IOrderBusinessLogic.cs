using Ocs.BLL.Dto.Orders;
using Ocs.Domain.Models;

namespace Ocs.BLL.Interfaces;

public interface IOrderBusinessLogic
{
    Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<OrderResponseDto?> AddOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken = default);

    Task<OrderResponseDto?> UpdateOrderAsync(Guid id, OrderUpdateDto orderDto, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
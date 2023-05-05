using Ocs.Domain.Models;

namespace Ocs.Application.Interfaces;

public interface IOrderApplication
{
    Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Order?> AddOrderAsync(Order orderDto, CancellationToken cancellationToken = default);

    Task<Order?> UpdateOrderAsync(Guid id, Order orderDto, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
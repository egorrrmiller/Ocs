using Ocs.Domain.Models;

namespace Ocs.Database.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> UpdateOrderAsync(Guid id, Order order, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
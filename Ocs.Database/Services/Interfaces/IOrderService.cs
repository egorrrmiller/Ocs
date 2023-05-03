using Ocs.Domain.Models;

namespace Ocs.Database.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> UpdateOrderAsync(Order order, ICollection<OrderLines>? orderLines, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Order order, CancellationToken cancellationToken = default);
}
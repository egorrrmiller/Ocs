﻿using Ocs.Domain.Models;

namespace Ocs.BLL.Interfaces;

public interface IOrderBusinessLogic
{
    Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> UpdateOrderAsync(Guid id, Order order, CancellationToken cancellationToken = default);

    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
}
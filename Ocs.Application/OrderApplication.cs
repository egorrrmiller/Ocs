using Ocs.Application.Interfaces;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;
using Ocs.Infrastructure.Services;
using Ocs.Infrastructure.Services.Interfaces;

namespace Ocs.Application;

public class OrderApplication : IOrderApplication
{
    private readonly ILineService _lineService;

    private readonly IOrderService _orderService;

    public OrderApplication(IOrderService orderService, ILineService lineService)
    {
        _orderService = orderService;
        _lineService = lineService;
    }

    /// <inheritdoc cref="OrderService.GetOrdersAsync" />
    public async Task<Order?> GetOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _orderService.GetOrdersAsync(id, cancellationToken);
    }

    /// <summary>
    /// Добавление нового заказа
    /// </summary>
    /// <param name="order"> </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <param name="orderDto"> Тело заказа </param>
    /// <returns> Созданный заказ </returns>
    public async Task<Order?> AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idExist = await GetOrdersAsync(order.Id, cancellationToken);

        if (idExist != null)
            throw new ArgumentException($"Id {order.Id} зарезервирован. Сгенерируйте новый.");

        var product = await _lineService.GetLinesAsync();

        foreach (var line in order.OrderLines)
        {
            if (!product.Exists(x => x.Id == line.LineId))
                throw new ArgumentException($"Товара с Id: {line.LineId} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть меньше 1");
        }

        return await _orderService.AddOrderAsync(order, cancellationToken);
    }

    /// <summary>
    /// Обновление заказа
    /// </summary>
    /// <param name="id"> Id заказа </param>
    /// <param name="order"> </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <param name="orderDto"> Тело обновленного заказа </param>
    /// <returns> Обновленный заказ </returns>
    /// <exception cref="ArgumentException"> В случае ошибки валидации </exception>
    public async Task<Order?> UpdateOrderAsync(Guid id, Order order, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var orderContext = await GetOrdersAsync(id, cancellationToken);

        var product = await _lineService.GetLinesAsync();

        if (orderContext == null)
            return null;

        if (orderContext.Status is OrderStatus.Paid or OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
            throw new ArgumentException("Заказы в статусах оплачен, передан в доставку, доставлен, завершен нельзя редактировать");

        foreach (var line in order.OrderLines)
        {
            if (!product.Exists(x => x.Id == line.LineId))
                throw new ArgumentException($"Товара с Id: {line.LineId} не существует");

            if (line.Qty < 1)
                throw new ArgumentException("Количество товаров не может быть меньше 1");
        }

        var orderLines = orderContext.OrderLines;

        orderContext.Status = order.Status;
        orderContext.OrderLines = order.OrderLines;

        return await _orderService.UpdateOrderAsync(orderContext, orderLines, cancellationToken);
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    /// <param name="id"> Id заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> true если удален, false если не удален </returns>
    /// <exception cref="ArgumentException"> в случае неверного статуса </exception>
    public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await GetOrdersAsync(id, cancellationToken);

        if (order == null)
            return false;

        order.Deleted = order.Status is OrderStatus.SentForDelivery or OrderStatus.Delivered or OrderStatus.Completed
            ? throw new ArgumentException("Заказы в статусах передан в доставку, доставлен, завершен нельзя удалить")
            : true;

        return await _orderService.DeleteOrderAsync(order, cancellationToken);
    }
}
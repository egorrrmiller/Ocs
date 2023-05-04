using Ocs.BLL.Dto.Orders;
using Ocs.BLL.Interfaces;
using Ocs.BLL.Mapping.Order;
using Ocs.Database.Services;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;

namespace Ocs.BLL;

public class OrderBusinessLogic : IOrderBusinessLogic
{
    private readonly ILineService _lineService;

    private readonly IOrderService _orderService;

    public OrderBusinessLogic(IOrderService orderService, ILineService lineService)
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
    /// <param name="orderDto"> Тело заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Созданный заказ </returns>
    public async Task<OrderResponseDto?> AddOrderAsync(OrderRequestDto orderDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = orderDto.MapToModel();
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

        var result = await _orderService.AddOrderAsync(order, cancellationToken);

        return result.MapToDto();
    }

    /// <summary>
    /// Обновление заказа
    /// </summary>
    /// <param name="id"> Id заказа </param>
    /// <param name="orderDto"> Тело обновленного заказа </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Обновленный заказ </returns>
    /// <exception cref="ArgumentException"> В случае ошибки валидации </exception>
    public async Task<OrderResponseDto?> UpdateOrderAsync(Guid id, OrderUpdateDto orderDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = orderDto.MapToModel();

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

        var result = await _orderService.UpdateOrderAsync(orderContext, orderLines, cancellationToken);

        return result.MapToDto();
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
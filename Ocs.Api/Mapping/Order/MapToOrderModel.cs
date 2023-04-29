using Ocs.Api.Dto.Orders;
using Ocs.Domain.Enums;
using Ocs.Domain.Models;

namespace Ocs.Api.Mapping.Order;

public static class MapToOrderModel
{
    public static Domain.Models.Order MapToModel(this OrderRequestDto orderRequest)
    {
        var order = new Domain.Models.Order
        {
            Id = orderRequest.Id,
            OrderProducts = orderRequest.Lines.Select(product => new OrderProduct
                {
                    ProductId = product.Id,
                    Qty = product.Qty
                })
                .ToList()
        };

        return order;
    }

    public static Domain.Models.Order MapToModel(this OrderUpdateDto orderUpdate)
    {
        var orderStatus = Enum.TryParse(orderUpdate.Status, out OrderStatus status)
            ? status
            : throw new ArgumentException("Некорретный статус заказа.");

        var order = new Domain.Models.Order
        {
            Status = orderStatus,
            OrderProducts = orderUpdate.Lines.Select(product => new OrderProduct
                {
                    ProductId = product.Id,
                    Qty = product.Qty
                })
                .ToList()
        };

        return order;
    }
}
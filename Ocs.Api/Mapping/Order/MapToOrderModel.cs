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
            OrderLines = orderRequest.Lines.Select(product => new OrderLines
                {
                    LineId = product.Id,
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
            OrderLines = orderUpdate.Lines.Select(line => new OrderLines
                {
                    LineId = line.Id,
                    Qty = line.Qty
                })
                .ToList()
        };

        return order;
    }
}
﻿using Ocs.Api.Dto.Orders;

namespace Ocs.Api.Mapping.Order;

public static class MapToOrderDto
{
    public static OrderResponseDto MapToDto(this Domain.Models.Order order) => new(order.Id, order.Status.ToString(),
        order.Created.ToString("yyyy-MM-dd HH:mm.s"), order.OrderLines
            .Select(product => new OrderLinesDto(product.LineId, product.Qty))
            .ToList());
}
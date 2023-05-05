using Microsoft.AspNetCore.Mvc;
using Ocs.Api.Dto.Orders;
using Ocs.Api.Mapping.Order;
using Ocs.Application.Interfaces;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderApplication _orderBusiness;

    public OrderController(IOrderApplication orderBusiness) => _orderBusiness = orderBusiness;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _orderBusiness.GetOrdersAsync(id, cancellationToken);

        if (order == null)
            return NotFound("Заказ не найден");

        return Ok(order.MapToDto());
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var order = await _orderBusiness.AddOrderAsync(orderRequestDto.MapToModel(), cancellationToken);

        return Ok(order.MapToDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateDto updateDto,
                                                 CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _orderBusiness.UpdateOrderAsync(id, updateDto.MapToModel(), cancellationToken);

        if (order == null)
            return NotFound("Заказ не найден");

        return Ok(order.MapToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var success = await _orderBusiness.DeleteOrderAsync(id, cancellationToken);

        if (!success)
            return NotFound("Заказ не найден");

        return Ok();
    }
}
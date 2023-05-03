using Microsoft.AspNetCore.Mvc;
using Ocs.BLL.Dto.Orders;
using Ocs.BLL.Interfaces;
using Ocs.BLL.Mapping.Order;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderBusinessLogic _orderBusiness;

    public OrderController(IOrderBusinessLogic orderBusiness) => _orderBusiness = orderBusiness;

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
        var order = await _orderBusiness.AddOrderAsync(orderRequestDto, cancellationToken);

        return Ok(order);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateDto updateDto,
                                                 CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var order = await _orderBusiness.UpdateOrderAsync(id, updateDto, cancellationToken);

        if (order == null)
            return NotFound("Заказ не найден");

        return Ok(order);
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
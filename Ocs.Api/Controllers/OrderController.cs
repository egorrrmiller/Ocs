using Microsoft.AspNetCore.Mvc;
using Ocs.Api.Dto.Orders;
using Ocs.Api.Mapping.Order;
using Ocs.BLL.Interfaces;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderBusinessLogic _orderBusiness;

    public OrderController(IOrderBusinessLogic orderService) => _orderBusiness = orderService;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _orderBusiness.GetOrdersAsync(id);

        if (order == null)
            return NotFound("Заказ не найден");

        return Ok(order.MapToDto());
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequestDto orderRequestDto)
    {
        var order = await _orderBusiness.AddOrderAsync(orderRequestDto.MapToModel());

        return Ok(order?.MapToDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateDto updateDto)
    {
        var order = await _orderBusiness.UpdateOrderAsync(id, updateDto.MapToModel());

        if (order == null)
            return NotFound("Заказ не найден");

        return Ok(order.MapToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var success = await _orderBusiness.DeleteOrderAsync(id);

        if (!success)
            return NotFound("Заказ не найден");

        return Ok();
    }
}
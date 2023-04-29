using Microsoft.AspNetCore.Mvc;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Dto.Order;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/routes")]
public class OrderController : ControllerBase
{
	private readonly IOrderService _orderService;

	public OrderController(IOrderService orderService) => _orderService = orderService;

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetOrder(Guid id)
	{
		var order = await _orderService.GetOrdersAsync(id);

		if (order == null)
		{
			return NotFound("Заказ не найден");
		}

		return Ok(order);
	}

	[HttpPost]
	public async Task<IActionResult> AddOrder(OrderDtoRequest orderDto)
	{
		var order = await _orderService.AddOrderAsync(orderDto);

		return Ok(order);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDtoUpdate dtoUpdate)
	{
		var order = await _orderService.UpdateOrderAsync(id, dtoUpdate);

		if (order == null)
		{
			return NotFound("Заказ не найден");
		}

		return Ok(order);
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> DeleteOrder(Guid id)
	{
		var success = await _orderService.DeleteOrderAsync(id);

		if (!success)
		{
			return NotFound("Заказ не найден");
		}

		return Ok();
	}
}
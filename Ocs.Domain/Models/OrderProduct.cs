namespace Ocs.Domain.Models;

public class OrderProduct
{
	public Guid OrderId { get; set; }

	public Guid ProductId { get; set; }

	public int Qty { get; set; }
}
namespace Ocs.Domain.Models;

public class OrderProduct
{
	public Guid OrderId { get; set; }

	public Guid ProductId { get; set; }

	public int Qty { get; set; }

	public Order Order { get; set; }

	public Product Product { get; set; }
}
using System.Text.Json.Serialization;
using Ocs.Domain.Enums;

namespace Ocs.Domain.Models;

public class Order
{
	public Guid Id { get; set; }

	public OrderStatus Status { get; set; }

	public DateTime Created { get; set; }

	public List<Product> Lines { get; set; }

	[JsonIgnore]
	[Newtonsoft.Json.JsonIgnore]
	public bool Deleted { get; set; }

	[JsonIgnore]
	[Newtonsoft.Json.JsonIgnore]
	public ICollection<OrderProduct>? OrderProducts { get; set; }
}
using Ocs.Domain.Enums;

namespace Ocs.Domain.Models;

public class Order
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime Created { get; set; }

    public List<Line> Lines { get; set; }

    public bool Deleted { get; set; }

    public ICollection<OrderLines>? OrderLines { get; set; }
}
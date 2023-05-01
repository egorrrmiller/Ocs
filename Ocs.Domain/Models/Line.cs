namespace Ocs.Domain.Models;

public class Line
{
    public Guid Id { get; set; }

    public List<Order>? Orders { get; set; }
}
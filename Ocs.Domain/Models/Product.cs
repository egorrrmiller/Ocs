namespace Ocs.Domain.Models;

public class Product
{
    public Guid Id { get; set; }

    public List<Order>? Orders { get; set; }
}
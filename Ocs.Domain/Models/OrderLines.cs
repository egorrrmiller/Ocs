namespace Ocs.Domain.Models;

public class OrderLines
{
    public Guid OrderId { get; set; }

    public Guid LineId { get; set; }

    public int Qty { get; set; }
}
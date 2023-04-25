using Newtonsoft.Json;
using Ocs.Domain.Enums;

namespace Ocs.Domain.Models;

public class Order
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime Created { get; set; }

    public ICollection<Product> Lines { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    public bool Deleted { get; set; }
}
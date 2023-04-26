using Newtonsoft.Json;

namespace Ocs.Domain.Models;

public class Product
{
    public Guid Id { get; set; }

    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Order>? Orders { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    public ICollection<OrderProduct>? OrderProducts { get; set; }
}
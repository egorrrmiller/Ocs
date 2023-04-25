using System.Text.Json;

namespace Ocs.Domain.Dto;

public class ErrorDto
{
    public string Message { get; set; }

    public int StatusCode { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Ocs.Api.Dto.Errors;

public class ErrorDto
{
    public string Message { get; set; }

    public int StatusCode { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    });
}
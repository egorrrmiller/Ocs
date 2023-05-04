using Ocs.Api.Dto.Lines;

namespace Ocs.Api.Mapping.Line;

public static class MapToLineDto
{
    public static IEnumerable<LineResponeDto> MapToDto(this List<Domain.Models.Line> lines) =>
        lines.Select(line => new LineResponeDto(line.Id));

    public static LineResponeDto MapToDto(this Domain.Models.Line lines) => new(lines.Id);
}
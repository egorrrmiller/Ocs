using Ocs.Api.Dto.Lines;

namespace Ocs.Api.Mapping.Line;

public static class MapToLineModel
{
    public static Domain.Models.Line MapToModel(this LineRequestDto lineRequestDto) => new()
    {
        Id = lineRequestDto.Id
    };
}
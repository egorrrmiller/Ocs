using Microsoft.AspNetCore.Mvc;
using Ocs.Api.Dto.Lines;
using Ocs.Api.Mapping.Line;
using Ocs.Application.Interfaces;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/lines")]
public class LineController : ControllerBase
{
    private readonly ILineApplication _lineBusiness;

    public LineController(ILineApplication lineBusiness) => _lineBusiness = lineBusiness;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok((await _lineBusiness.GetLinesAsync()).MapToDto());

    [HttpPost]
    public async Task<IActionResult> AddLine(LineRequestDto lineRequestDto)
    {
        var line = await _lineBusiness.AddLineAsync(lineRequestDto.MapToModel());

        if (line == null)
        {
            return BadRequest("Строка уже существует");
        }

        return Ok(line.MapToDto());
    }
}
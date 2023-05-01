using Microsoft.AspNetCore.Mvc;
using Ocs.Api.Dto.Lines;
using Ocs.Database.Services.Interfaces;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/lines")]
public class LineController : ControllerBase
{
    private readonly ILineService _lineService;

    public LineController(ILineService lineService) => _lineService = lineService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _lineService.GetLinesAsync());

    [HttpPost]
    public async Task<IActionResult> AddLine(LineRequestDto lineRequestDto)
    {
        var line = await _lineService.AddLineAsync(new()
        {
            Id = lineRequestDto.Id
        });

        if (line == null)
            return BadRequest("Строка уже существует");

        return Ok(line);
    }
}
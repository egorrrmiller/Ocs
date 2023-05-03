using Microsoft.AspNetCore.Mvc;
using Ocs.Api.Dto.Lines;
using Ocs.BLL.Interfaces;

namespace Ocs.Api.Controllers;

[ApiController]
[Route("/lines")]
public class LineController : ControllerBase
{
    private readonly ILineBusinessLogic _lineBusiness;

    public LineController(ILineBusinessLogic lineBusiness) => _lineBusiness = lineBusiness;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _lineBusiness.GetLinesAsync());

    [HttpPost]
    public async Task<IActionResult> AddLine(LineRequestDto lineRequestDto)
    {
        var line = await _lineBusiness.AddLineAsync(new()
        {
            Id = lineRequestDto.Id
        });

        if (line == null)
            return BadRequest("Строка уже существует");

        return Ok(line);
    }
}
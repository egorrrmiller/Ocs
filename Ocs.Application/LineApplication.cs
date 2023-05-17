using Microsoft.EntityFrameworkCore;
using Ocs.Application.Interfaces;
using Ocs.Domain.Models;
using Ocs.Infrastructure.Context;
using Ocs.Infrastructure.Services.Interfaces;

namespace Ocs.Application;

public class LineApplication : ILineApplication
{
    private readonly OcsContext _context;

    private readonly ILineService _lineService;

    public LineApplication(ILineService lineService, OcsContext context)
    {
        _lineService = lineService;
        _context = context;
    }

    /// <summary>
    /// Получение всех строк
    /// </summary>
    /// <returns> Список строк </returns>
    public async Task<List<Line>> GetLinesAsync() => await _lineService.GetLinesAsync();

    /// <summary>
    /// Добавление новой строки
    /// </summary>
    /// <param name="line"> Тело строки </param>
    /// <returns> Добавленная строка </returns>
    public async Task<Line?> AddLineAsync(Line line)
    {
        var lineExists = await _context.Line.FirstOrDefaultAsync(id => id.Id == line.Id);

        if (lineExists != null)
        {
            return null;
        }

        return await _lineService.AddLineAsync(line);
    }
}
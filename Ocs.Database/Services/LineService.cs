using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Models;

namespace Ocs.Database.Services;

public class LineService : ILineService
{
    private readonly OcsContext _context;

    public LineService(OcsContext context) => _context = context;

    /// <summary>
    /// Получение всех строк
    /// </summary>
    /// <returns> Список строк </returns>
    public async Task<List<Line>> GetLinesAsync() => await _context.Line.ToListAsync();

    /// <summary>
    /// Добавление новой строки
    /// </summary>
    /// <param name="line"> Тело строки </param>
    /// <returns> Добавленная строка </returns>
    public async Task<Line?> AddLineAsync(Line line)
    {
        var lineUpdate = await _context.Line.AddAsync(line);

        await _context.SaveChangesAsync();

        return lineUpdate.Entity;
    }
}
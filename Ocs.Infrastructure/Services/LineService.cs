using Microsoft.EntityFrameworkCore;
using Ocs.Domain.Models;
using Ocs.Infrastructure.Context;
using Ocs.Infrastructure.Services.Interfaces;

namespace Ocs.Infrastructure.Services;

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
using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Models;

namespace Ocs.Database.Services;

public class LineService : ILineService
{
    private readonly OcsContext _context;

    public LineService(OcsContext context) => _context = context;

    public async Task<List<Line>> GetLinesAsync() => await _context.Line.ToListAsync();

    public async Task<Line?> AddLineAsync(Line line)
    {
        var lineUpdate = await _context.Line.AddAsync(line);

        await _context.SaveChangesAsync();

        return lineUpdate.Entity;
    }
}
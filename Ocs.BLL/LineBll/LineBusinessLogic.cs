﻿using Microsoft.EntityFrameworkCore;
using Ocs.BLL.Interfaces;
using Ocs.Database.Context;
using Ocs.Database.Services.Interfaces;
using Ocs.Domain.Models;

namespace Ocs.BLL.LineBll;

public class LineBusinessLogic : ILineBusinessLogic
{
    private readonly OcsContext _context;

    private readonly ILineService _lineService;

    public LineBusinessLogic(ILineService lineService, OcsContext context)
    {
        _lineService = lineService;
        _context = context;
    }

    public async Task<List<Line>> GetLinesAsync() => await _lineService.GetLinesAsync();

    public async Task<Line?> AddLineAsync(Line line)
    {
        var lineExists = await _context.Line.FirstOrDefaultAsync(id => id.Id == line.Id);

        if (lineExists == null)
            return null;

        return await _lineService.AddLineAsync(line);
    }
}
using Ocs.Domain.Models;

namespace Ocs.Database.Services.Interfaces;

public interface ILineService
{
    Task<List<Line>> GetLinesAsync();

    Task<Line?> AddLineAsync(Line line);
}
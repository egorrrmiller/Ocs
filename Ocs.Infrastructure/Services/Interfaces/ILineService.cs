using Ocs.Domain.Models;

namespace Ocs.Infrastructure.Services.Interfaces;

public interface ILineService
{
    Task<List<Line>> GetLinesAsync();

    Task<Line?> AddLineAsync(Line line);
}
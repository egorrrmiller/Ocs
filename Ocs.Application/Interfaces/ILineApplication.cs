using Ocs.Domain.Models;

namespace Ocs.Application.Interfaces;

public interface ILineApplication
{
    Task<List<Line>> GetLinesAsync();

    Task<Line?> AddLineAsync(Line line);
}
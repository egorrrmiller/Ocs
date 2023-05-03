using Ocs.Domain.Models;

namespace Ocs.BLL.Interfaces;

public interface ILineBusinessLogic
{
    Task<List<Line>> GetLinesAsync();

    Task<Line?> AddLineAsync(Line line);
}
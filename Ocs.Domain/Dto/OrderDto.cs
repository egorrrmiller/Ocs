using Ocs.Domain.Models;

namespace Ocs.Domain.Dto;

public record OrderDto(Guid Id, List<Product> Lines);

public record OrderDtoResponse(Guid Id, string Status, string Created, ICollection<Product> Lines);

public record OrderDtoUpdate(string Status, List<Product> Lines);
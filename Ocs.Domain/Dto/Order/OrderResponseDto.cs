using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderResponseDto(Guid Id, string Status, string Created, List<ProductResponseDto> Lines);
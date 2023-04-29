using Ocs.Dto.Product;

namespace Ocs.Dto.Order;

public record OrderResponseDto(Guid Id, string Status, string Created, List<ProductResponseDto> Lines);
using Ocs.Dto.Product;

namespace Ocs.Dto.Order;

public record OrderRequestDto(Guid Id, List<ProductResponseDto> Lines);
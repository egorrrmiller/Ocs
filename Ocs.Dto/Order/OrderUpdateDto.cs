using Ocs.Dto.Product;

namespace Ocs.Dto.Order;

public record OrderUpdateDto(string Status, List<ProductResponseDto> Lines);
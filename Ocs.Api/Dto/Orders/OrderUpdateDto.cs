using Ocs.Api.Dto.Products;

namespace Ocs.Api.Dto.Orders;

public record OrderUpdateDto(string Status, List<ProductResponseDto> Lines);
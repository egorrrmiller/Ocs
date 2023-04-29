using Ocs.Api.Dto.Products;

namespace Ocs.Api.Dto.Orders;

public record OrderRequestDto(Guid Id, List<ProductResponseDto> Lines);
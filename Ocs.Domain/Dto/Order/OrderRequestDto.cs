using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderRequestDto(Guid Id, List<ProductDtoResponse> Lines);
using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderUpdateDto(string Status, List<ProductDtoResponse> Lines);
using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderDtoUpdate(string Status, List<ProductDtoResponse> Lines);
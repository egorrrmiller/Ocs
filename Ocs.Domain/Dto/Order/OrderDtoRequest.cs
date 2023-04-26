using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderDtoRequest(Guid Id, List<ProductDtoResponse> Lines);
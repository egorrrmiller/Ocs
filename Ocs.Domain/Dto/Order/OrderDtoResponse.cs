using Ocs.Domain.Dto.Product;

namespace Ocs.Domain.Dto.Order;

public record OrderDtoResponse(Guid Id, string Status, string Created, List<ProductDtoResponse> Lines);
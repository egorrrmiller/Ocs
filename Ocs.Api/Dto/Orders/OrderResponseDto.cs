namespace Ocs.Api.Dto.Orders;

public record OrderResponseDto(Guid Id, string Status, string Created, List<OrderLinesDto> Lines);
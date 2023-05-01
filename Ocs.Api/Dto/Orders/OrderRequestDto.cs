namespace Ocs.Api.Dto.Orders;

public record OrderRequestDto(Guid Id, List<OrderLinesDto> Lines);
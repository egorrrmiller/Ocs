namespace Ocs.Api.Dto.Orders;

public record OrderUpdateDto(string Status, List<OrderLinesDto> Lines);
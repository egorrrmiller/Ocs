namespace Ocs.BLL.Dto.Orders;

public record OrderRequestDto(Guid Id, List<OrderLinesDto> Lines);
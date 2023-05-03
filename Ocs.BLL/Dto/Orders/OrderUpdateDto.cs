namespace Ocs.BLL.Dto.Orders;

public record OrderUpdateDto(string Status, List<OrderLinesDto> Lines);
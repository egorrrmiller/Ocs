namespace Ocs.Domain.Dto;

public record OrderDtoRequest(Guid Id, List<ProductDtoResponse> Lines);

public record OrderDtoResponse(Guid Id, string Status, string Created, List<ProductDtoResponse> Lines);

public record OrderDtoUpdate(string Status, List<ProductDtoResponse> Lines);
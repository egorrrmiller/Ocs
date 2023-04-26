namespace Ocs.Domain.Dto;

public record ProductDtoRequest(Guid Id);

public record ProductDtoResponse(Guid Id, int Qty);
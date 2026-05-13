using MediatR;

namespace Application.UseCase.Flights;

public sealed record UpdateFlightState(
    int FlightId,
    int NuevoEstadoId,
    int? CambiadoPor,
    string? Observacion,
    DateTime? ReprogramadoEn = null
) : IRequest;

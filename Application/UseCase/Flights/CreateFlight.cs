using MediatR;

namespace Application.UseCase.Flights;

public sealed record CreateFlight(
    string CodigoVuelo,
    int AerolineaId,
    int RutaId,
    int AeronaveId,
    DateTime FechaSalida,
    DateTime FechaLlegadaEstimada,
    int CapacidadTotal,
    int EstadoVueloId
) : IRequest<int>;

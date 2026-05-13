using MediatR;

namespace Application.UseCase.Routes;

public sealed record CreateRoute(
    int AeropuertoOrigenId,
    int AeropuertoDestinoId,
    int? DistanciaKm,
    int? DuracionEstimadaMin
) : IRequest<int>;

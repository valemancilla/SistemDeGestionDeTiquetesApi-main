using Application.Abstractions;
using Domain.Entities.Routes;
using MediatR;

namespace Application.UseCase.Routes;

public sealed class CreateRouteHandler : IRequestHandler<CreateRoute, int>
{
    private readonly IUnitOfWork _uow;

    public CreateRouteHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateRoute req, CancellationToken ct)
    {
        if (req.AeropuertoOrigenId == req.AeropuertoDestinoId)
            throw new InvalidOperationException("El origen y destino no pueden ser el mismo aeropuerto.");

        if (await _uow.Routes.ExistsAsync(req.AeropuertoOrigenId, req.AeropuertoDestinoId, ct))
            throw new InvalidOperationException("Ya existe esta ruta.");

        var route = new FlightRoute(req.AeropuertoOrigenId, req.AeropuertoDestinoId,
            req.DistanciaKm, req.DuracionEstimadaMin);

        await _uow.Routes.AddAsync(route, ct);
        await _uow.SaveChangesAsync(ct);
        return route.Id;
    }
}

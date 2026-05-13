using Application.Abstractions;
using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed class CreateFlightHandler : IRequestHandler<CreateFlight, int>
{
    private readonly IUnitOfWork _uow;

    public CreateFlightHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateFlight req, CancellationToken ct)
    {
        if (await _uow.Flights.ExistsCodigoAsync(req.CodigoVuelo, ct))
            throw new InvalidOperationException($"Ya existe un vuelo con código '{req.CodigoVuelo}'.");

        if (req.FechaLlegadaEstimada <= req.FechaSalida)
            throw new InvalidOperationException("La fecha de llegada debe ser posterior a la de salida.");

        var flight = new Flight(
            req.CodigoVuelo, req.AerolineaId, req.RutaId, req.AeronaveId,
            req.FechaSalida, req.FechaLlegadaEstimada, req.CapacidadTotal, req.EstadoVueloId);

        await _uow.Flights.AddAsync(flight, ct);
        await _uow.SaveChangesAsync(ct);
        return flight.Id;
    }
}

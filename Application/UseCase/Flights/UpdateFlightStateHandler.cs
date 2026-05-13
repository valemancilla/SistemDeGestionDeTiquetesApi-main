using Application.Abstractions;
using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed class UpdateFlightStateHandler : IRequestHandler<UpdateFlightState>
{
    private readonly IUnitOfWork _uow;

    public UpdateFlightStateHandler(IUnitOfWork uow) => _uow = uow;

    public async Task Handle(UpdateFlightState req, CancellationToken ct)
    {
        var flight = await _uow.Flights.GetByIdAsync(req.FlightId, ct)
            ?? throw new InvalidOperationException($"Vuelo {req.FlightId} no encontrado.");

        var history = new FlightHistory(
            flight.Id, flight.EstadoVueloId, req.NuevoEstadoId,
            req.CambiadoPor, req.Observacion);

        flight.CambiarEstado(req.NuevoEstadoId, req.ReprogramadoEn);

        await _uow.Flights.UpdateAsync(flight, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

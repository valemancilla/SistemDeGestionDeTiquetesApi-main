using Domain.Common;

namespace Domain.Entities.Flights;

public sealed class FlightStatusTransition : BaseEntity<int>
{
    public int EstadoOrigenId { get; private set; }
    public int EstadoDestinoId { get; private set; }

    public FlightState? EstadoOrigen { get; private set; }
    public FlightState? EstadoDestino { get; private set; }

    private FlightStatusTransition() { }

    public FlightStatusTransition(int estadoOrigenId, int estadoDestinoId)
    {
        EstadoOrigenId = estadoOrigenId;
        EstadoDestinoId = estadoDestinoId;
    }
}

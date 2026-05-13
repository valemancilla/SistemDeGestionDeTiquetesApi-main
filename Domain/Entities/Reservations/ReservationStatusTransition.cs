using Domain.Common;

namespace Domain.Entities.Reservations;

public sealed class ReservationStatusTransition : BaseEntity<int>
{
    public int EstadoOrigenId { get; private set; }
    public int EstadoDestinoId { get; private set; }

    public ReservationStatus? EstadoOrigen { get; private set; }
    public ReservationStatus? EstadoDestino { get; private set; }

    private ReservationStatusTransition() { }

    public ReservationStatusTransition(int estadoOrigenId, int estadoDestinoId)
    {
        EstadoOrigenId = estadoOrigenId;
        EstadoDestinoId = estadoDestinoId;
    }
}

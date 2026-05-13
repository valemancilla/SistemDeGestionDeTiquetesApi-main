using Domain.Common;
using Domain.Entities.Reservations;
using Domain.ValueObjects.Tickets;

namespace Domain.Entities.Tickets;

public sealed class Ticket : BaseEntity<int>
{
    public int ReservaPasajeroId { get; private set; }
    public TicketCode CodigoTiquete { get; private set; } = default!;
    public DateTime FechaEmision { get; private set; }
    public int EstadoTiqueteId { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public ReservationPassenger? ReservaPasajero { get; private set; }
    public TicketStatus? EstadoTiquete { get; private set; }

    private Ticket() { }

    public Ticket(int reservaPasajeroId, string codigoTiquete, DateTime fechaEmision, int estadoTiqueteId)
    {
        ReservaPasajeroId = reservaPasajeroId;
        CodigoTiquete = TicketCode.Create(codigoTiquete);
        FechaEmision = fechaEmision;
        EstadoTiqueteId = estadoTiqueteId;
    }

    public void CambiarEstado(int nuevoEstadoId)
    {
        EstadoTiqueteId = nuevoEstadoId;
        ActualizadoEn = DateTime.UtcNow;
    }
}

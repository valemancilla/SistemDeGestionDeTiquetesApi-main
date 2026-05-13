using Domain.Common;

namespace Domain.Entities.Tickets;

public sealed class TicketStatus : LookupEntity
{
    private TicketStatus() { }

    public TicketStatus(string nombre) => Nombre = nombre;
}

using Domain.Entities.Tickets;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>ticketstatuses</c>. Entidad: <see cref="TicketStatus"/>.</summary>
public static class TicketStatuses
{
    public const string TableName = "ticket_statuses";
    public const int NombreMaxLength = 50;

    public const int EmitidoId = 1;
    public const string EmitidoNombre = "Emitido";

    public const int UsadoId = 2;
    public const string UsadoNombre = "Usado";

    public const int CanceladoId = 3;
    public const string CanceladoNombre = "Cancelado";

    public const int ReembolsadoId = 4;
    public const string ReembolsadoNombre = "Reembolsado";
}

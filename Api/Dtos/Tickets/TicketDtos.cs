namespace Api.Dtos.Tickets;

public sealed record TicketDto(
    int Id,
    int ReservaPasajeroId,
    string CodigoTiquete,
    DateTime FechaEmision,
    int EstadoTiqueteId,
    string? NombreEstado,
    DateTime CreadoEn);

using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Tickets;

public sealed record CreateTicketRequest(
    [Range(1, int.MaxValue)] int ReservaPasajeroId,
    [Required][StringLength(30, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El código de tiquete admite 1 a 30 letras o números.")]
    string CodigoTiquete,
    DateTime FechaEmision,
    [Range(1, int.MaxValue)] int EstadoTiqueteId);

public sealed record UpdateTicketStatusRequest(
    [Range(1, int.MaxValue)] int NuevoEstadoId);

using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Reservations;

public sealed record CreateReservationRequest(
    [Required][StringLength(30, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El código de reserva admite 1 a 30 letras o números.")]
    string CodigoReserva,
    [Range(1, int.MaxValue)] int ClienteId,
    [Range(1, int.MaxValue)] int EstadoReservaId,
    [Range(0, double.MaxValue)] decimal ValorTotal,
    DateTime? VenceEn);

public sealed record UpdateReservationRequest(
    [Range(0, double.MaxValue)] decimal ValorTotal,
    DateTime? VenceEn);

public sealed record UpdateReservationStatusRequest(
    [Range(1, int.MaxValue)] int NuevoEstadoId);

public sealed record CreateReservationFlightRequest(
    [Range(1, int.MaxValue)] int ReservaId,
    [Range(1, int.MaxValue)] int VueloId,
    [Range(0, double.MaxValue)] decimal ValorParcial);

public sealed record UpdateReservationFlightRequest(
    [Range(0, double.MaxValue)] decimal ValorParcial);

public sealed record CreateReservationPassengerRequest(
    [Range(1, int.MaxValue)] int ReservaVueloId,
    [Range(1, int.MaxValue)] int PasajeroId);

public sealed record CreateReservationStatusTransitionRequest(
    [Range(1, int.MaxValue)] int EstadoOrigenId,
    [Range(1, int.MaxValue)] int EstadoDestinoId);

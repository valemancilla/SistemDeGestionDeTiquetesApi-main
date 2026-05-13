namespace Api.Dtos.Reservations;

public sealed record ReservationDto(
    int Id,
    string CodigoReserva,
    int ClienteId,
    int EstadoReservaId,
    string? NombreEstado,
    decimal ValorTotal,
    DateTime? VenceEn,
    DateTime FechaReserva);

public sealed record ReservationFlightDto(
    int Id,
    int ReservaId,
    int VueloId,
    string? CodigoVuelo,
    decimal ValorParcial);

public sealed record ReservationPassengerDto(
    int Id,
    int ReservaVueloId,
    int PasajeroId,
    string? NombresPasajero,
    string? ApellidosPasajero);

public sealed record ReservationStatusTransitionDto(
    int Id,
    int EstadoOrigenId,
    string? NombreEstadoOrigen,
    int EstadoDestinoId,
    string? NombreEstadoDestino);

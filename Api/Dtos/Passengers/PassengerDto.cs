namespace Api.Dtos.Passengers;

public sealed record PassengerDto(
    int Id,
    int PersonaId,
    string? Nombres,
    string? Apellidos,
    int TipoPasajeroId,
    string? NombreTipoPasajero);

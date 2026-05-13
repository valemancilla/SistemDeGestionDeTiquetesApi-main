namespace Api.Dtos.Airport;

public sealed record AirportAirlineDto(
    int Id,
    int AeropuertoId,
    string? NombreAeropuerto,
    int AerolineaId,
    string? NombreAerolinea,
    string? Terminal,
    DateOnly FechaInicio,
    DateOnly? FechaFin,
    bool Activa);

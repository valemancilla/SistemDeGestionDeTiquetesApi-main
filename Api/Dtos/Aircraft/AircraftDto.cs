namespace Api.Dtos.Aircraft;

public sealed record AircraftDto(
    int Id,
    string Matricula,
    int ModeloId,
    string? NombreModelo,
    int AerolineaId,
    string? NombreAerolinea,
    DateOnly? FechaFabricacion,
    bool Activa);

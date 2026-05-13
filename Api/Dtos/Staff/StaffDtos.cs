namespace Api.Dtos.Staff;

public sealed record StaffDto(
    int Id,
    int PersonaId,
    string? Nombres,
    string? Apellidos,
    int CargoId,
    string? NombreCargo,
    int? AerolineaId,
    string? NombreAerolinea,
    int? AeropuertoId,
    string? NombreAeropuerto,
    DateOnly FechaIngreso,
    bool Activo);

public sealed record StaffAvailabilityDto(
    int Id,
    int PersonalId,
    int EstadoDisponibilidadId,
    string? NombreEstado,
    DateTime FechaInicio,
    DateTime FechaFin,
    string? Observacion);

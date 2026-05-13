namespace Api.Dtos.Maintenance;

public sealed record MaintenanceTypeDto(int Id, string Nombre);

public sealed record AircraftMaintenanceDto(
    int Id,
    int AeronaveId,
    string? MatriculaAeronave,
    int TipoMantenimientoId,
    string? NombreTipoMantenimiento,
    DateTime FechaInicio,
    DateTime? FechaFin,
    string? Descripcion);

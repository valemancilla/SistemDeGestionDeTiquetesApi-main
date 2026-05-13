using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Maintenance;

public sealed record CreateMaintenanceTypeRequest(
    [Required][MaxLength(100)] string Nombre);

public sealed record UpdateMaintenanceTypeRequest(
    [Required][MaxLength(100)] string Nombre);

public sealed record CreateAircraftMaintenanceRequest(
    [Range(1, int.MaxValue)] int AeronaveId,
    [Range(1, int.MaxValue)] int TipoMantenimientoId,
    DateTime FechaInicio,
    [MaxLength(500)] string? Descripcion);

public sealed record UpdateAircraftMaintenanceRequest(
    [Range(1, int.MaxValue)] int TipoMantenimientoId,
    DateTime FechaInicio,
    DateTime? FechaFin,
    [MaxLength(500)] string? Descripcion);

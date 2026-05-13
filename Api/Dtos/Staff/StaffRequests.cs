using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Staff;

public sealed record CreateStaffRequest(
    [Range(1, int.MaxValue)] int PersonaId,
    [Range(1, int.MaxValue)] int CargoId,
    DateOnly FechaIngreso,
    int? AerolineaId,
    int? AeropuertoId);

public sealed record UpdateStaffRequest(
    [Range(1, int.MaxValue)] int CargoId,
    int? AerolineaId,
    int? AeropuertoId,
    bool Activo);

public sealed record CreateStaffAvailabilityRequest(
    [Range(1, int.MaxValue)] int PersonalId,
    [Range(1, int.MaxValue)] int EstadoDisponibilidadId,
    DateTime FechaInicio,
    DateTime FechaFin,
    [MaxLength(300)] string? Observacion);

public sealed record UpdateStaffAvailabilityRequest(
    [Range(1, int.MaxValue)] int EstadoDisponibilidadId,
    DateTime FechaInicio,
    DateTime FechaFin,
    [MaxLength(300)] string? Observacion);

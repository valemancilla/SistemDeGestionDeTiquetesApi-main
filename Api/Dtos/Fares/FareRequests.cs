using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Fares;

public sealed record CreateSeasonRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(0.01, 100.0)] decimal PrecioFactor,
    [MaxLength(300)] string? Descripcion);

public sealed record UpdateSeasonRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(0.01, 100.0)] decimal PrecioFactor,
    [MaxLength(300)] string? Descripcion);

public sealed record CreatePassengerTypeRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(0, 120)] int? EdadMin,
    [Range(0, 120)] int? EdadMax);

public sealed record UpdatePassengerTypeRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(0, 120)] int? EdadMin,
    [Range(0, 120)] int? EdadMax);

public sealed record CreateFareRequest(
    [Range(1, int.MaxValue)] int RutaId,
    [Range(1, int.MaxValue)] int TipoCabinaId,
    [Range(1, int.MaxValue)] int TipoPasajeroId,
    [Range(1, int.MaxValue)] int TemporadaId,
    [Range(0.01, double.MaxValue)] decimal PrecioBase,
    DateOnly? VigenciaDesde,
    DateOnly? VigenciaHasta);

public sealed record UpdateFareRequest(
    [Range(0.01, double.MaxValue)] decimal PrecioBase,
    DateOnly? VigenciaDesde,
    DateOnly? VigenciaHasta);

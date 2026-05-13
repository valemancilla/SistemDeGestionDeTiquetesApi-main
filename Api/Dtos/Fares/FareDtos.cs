namespace Api.Dtos.Fares;

public sealed record SeasonDto(
    int Id,
    string Nombre,
    string? Descripcion,
    decimal PrecioFactor);

public sealed record PassengerTypeDto(
    int Id,
    string Nombre,
    int? EdadMin,
    int? EdadMax);

public sealed record FareDto(
    int Id,
    int RutaId,
    int TipoCabinaId,
    string? NombreTipoCabina,
    int TipoPasajeroId,
    string? NombreTipoPasajero,
    int TemporadaId,
    string? NombreTemporada,
    decimal PrecioBase,
    DateOnly? VigenciaDesde,
    DateOnly? VigenciaHasta);

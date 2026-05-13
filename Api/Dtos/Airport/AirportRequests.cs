using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Airport;

public sealed record CreateAirportRequest(
    [Required][MaxLength(150)] string Nombre,
    [Required][StringLength(3, MinimumLength = 3)][RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "El código IATA debe ser exactamente 3 letras.")]
    string CodigoIata,
    [Range(1, int.MaxValue)] int CiudadId,
    [RegularExpression(@"^$|^[A-Za-z]{4}$", ErrorMessage = "El código ICAO debe ser 4 letras o vacío.")]
    string? CodigoIcao);

public sealed record UpdateAirportRequest(
    [Required][MaxLength(150)] string Nombre,
    [RegularExpression(@"^$|^[A-Za-z]{4}$", ErrorMessage = "El código ICAO debe ser 4 letras o vacío.")]
    string? CodigoIcao);

public sealed record CreateAirportAirlineRequest(
    [Range(1, int.MaxValue)] int AeropuertoId,
    [Range(1, int.MaxValue)] int AerolineaId,
    DateOnly FechaInicio,
    [RegularExpression(@"^$|^[A-Za-z0-9 .\-]{1,20}$", ErrorMessage = "Terminal: hasta 20 caracteres alfanuméricos, espacio, punto o guion.")]
    string? Terminal,
    DateOnly? FechaFin);

public sealed record UpdateAirportAirlineRequest(
    [RegularExpression(@"^$|^[A-Za-z0-9 .\-]{1,20}$", ErrorMessage = "Terminal: hasta 20 caracteres alfanuméricos, espacio, punto o guion.")]
    string? Terminal,
    DateOnly? FechaFin,
    bool Activa);

namespace Api.Dtos.Airport;

public sealed record AirportDto(
    int Id,
    string Nombre,
    string CodigoIata,
    string? CodigoIcao,
    int CiudadId,
    string? NombreCiudad);

namespace Api.Dtos.Routes;

public sealed record RouteDto(
    int Id,
    int AeropuertoOrigenId,
    string? NombreOrigen,
    string? CodigoIataOrigen,
    int AeropuertoDestinoId,
    string? NombreDestino,
    string? CodigoIataDestino,
    int? DistanciaKm,
    int? DuracionEstimadaMin);

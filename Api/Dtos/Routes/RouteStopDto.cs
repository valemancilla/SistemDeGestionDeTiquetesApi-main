namespace Api.Dtos.Routes;

public sealed record RouteStopDto(
    int Id,
    int RutaId,
    int AeropuertoEscalaId,
    string? NombreAeropuertoEscala,
    string? CodigoIataEscala,
    int Orden,
    int DuracionEscalaMin);

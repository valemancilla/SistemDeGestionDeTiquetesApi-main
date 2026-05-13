using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Routes;

public sealed record CreateRouteRequest(
    [Range(1, int.MaxValue)] int AeropuertoOrigenId,
    [Range(1, int.MaxValue)] int AeropuertoDestinoId,
    [Range(1, int.MaxValue)] int? DistanciaKm,
    [Range(1, int.MaxValue)] int? DuracionEstimadaMin);

public sealed record UpdateRouteRequest(
    [Range(1, int.MaxValue)] int? DistanciaKm,
    [Range(1, int.MaxValue)] int? DuracionEstimadaMin);

public sealed record CreateRouteStopRequest(
    [Range(1, int.MaxValue)] int RutaId,
    [Range(1, int.MaxValue)] int AeropuertoEscalaId,
    [Range(1, int.MaxValue)] int Orden,
    [Range(0, int.MaxValue)] int DuracionEscalaMin);

public sealed record UpdateRouteStopRequest(
    [Range(1, int.MaxValue)] int Orden,
    [Range(0, int.MaxValue)] int DuracionEscalaMin);

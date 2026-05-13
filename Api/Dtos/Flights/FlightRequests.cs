using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Flights;

public sealed record CreateFlightRequest(
    [Required][StringLength(10, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Código de vuelo: 1 a 10 caracteres (letras, números o guion).")]
    string CodigoVuelo,
    [Range(1, int.MaxValue)] int AerolineaId,
    [Range(1, int.MaxValue)] int RutaId,
    [Range(1, int.MaxValue)] int AeronaveId,
    DateTime FechaSalida,
    DateTime FechaLlegadaEstimada,
    [Range(1, int.MaxValue)] int CapacidadTotal,
    [Range(1, int.MaxValue)] int EstadoVueloId);

public sealed record UpdateFlightRequest(
    [Range(1, int.MaxValue)] int AeronaveId,
    DateTime FechaSalida,
    DateTime FechaLlegadaEstimada);

public sealed record UpdateFlightStateRequest(
    [Range(1, int.MaxValue)] int NuevoEstadoId,
    int? CambiadoPor,
    [MaxLength(500)] string? Observacion,
    DateTime? ReprogramadoEn);

public sealed record CreateFlightHistoryRequest(
    [Range(1, int.MaxValue)] int VueloId,
    [Range(1, int.MaxValue)] int EstadoAnteriorId,
    [Range(1, int.MaxValue)] int EstadoNuevoId,
    int? CambiadoPor,
    [MaxLength(500)] string? Observacion);

public sealed record CreateFlightStatusTransitionRequest(
    [Range(1, int.MaxValue)] int EstadoOrigenId,
    [Range(1, int.MaxValue)] int EstadoDestinoId);

public sealed record CreateFlightAssignmentRequest(
    [Range(1, int.MaxValue)] int VueloId,
    [Range(1, int.MaxValue)] int PersonalId,
    [Range(1, int.MaxValue)] int RolVueloId);

public sealed record UpdateFlightAssignmentRequest(
    [Range(1, int.MaxValue)] int RolVueloId);

public sealed record CreateFlightSeatRequest(
    [Range(1, int.MaxValue)] int VueloId,
    [Required][StringLength(5, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "El código de asiento admite 1 a 5 letras o números.")]
    string CodigoAsiento,
    [Range(1, int.MaxValue)] int TipoCabinaId,
    [Range(1, int.MaxValue)] int TipoUbicacionId);

public sealed record UpdateFlightSeatRequest(bool EstaOcupado);

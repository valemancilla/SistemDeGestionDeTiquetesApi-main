namespace Api.Dtos.Flights;

public sealed record FlightHistoryDto(
    int Id,
    int VueloId,
    int EstadoAnteriorId,
    string? NombreEstadoAnterior,
    int EstadoNuevoId,
    string? NombreEstadoNuevo,
    int? CambiadoPor,
    DateTime FechaCambio,
    string? Observacion);

public sealed record FlightStatusTransitionDto(
    int Id,
    int EstadoOrigenId,
    string? NombreEstadoOrigen,
    int EstadoDestinoId,
    string? NombreEstadoDestino);

public sealed record FlightAssignmentDto(
    int Id,
    int VueloId,
    int PersonalId,
    string? NombrePersonal,
    int RolVueloId,
    string? NombreRolVuelo);

public sealed record FlightSeatDto(
    int Id,
    int VueloId,
    string CodigoAsiento,
    int TipoCabinaId,
    string? NombreTipoCabina,
    int TipoUbicacionId,
    string? NombreTipoUbicacion,
    bool EstaOcupado);

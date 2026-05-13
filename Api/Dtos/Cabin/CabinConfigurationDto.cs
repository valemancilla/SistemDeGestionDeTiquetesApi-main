namespace Api.Dtos.Cabin;

public sealed record CabinConfigurationDto(
    int Id,
    int AeronaveId,
    string? NombreAeronave,
    int TipoCabinaId,
    string? NombreTipoCabina,
    int FilaInicio,
    int FilaFin,
    int AsientosPorFila,
    string LetrasAsientos);

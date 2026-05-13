namespace Api.Dtos.CheckIn;

public sealed record CheckInDto(
    int Id,
    int TiqueteId,
    int PersonalId,
    int AsientoVueloId,
    string? CodigoAsiento,
    DateTime FechaCheckin,
    int EstadoCheckinId,
    string? NombreEstado,
    string NumeroTarjetaEmbarque,
    bool EquipajeBodega,
    decimal? PesoEquipajeKg);

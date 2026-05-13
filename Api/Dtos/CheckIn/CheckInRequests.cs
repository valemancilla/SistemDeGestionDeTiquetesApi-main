using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.CheckIn;

public sealed record CreateCheckInRequest(
    [Range(1, int.MaxValue)] int TiqueteId,
    [Range(1, int.MaxValue)] int PersonalId,
    [Range(1, int.MaxValue)] int AsientoVueloId,
    DateTime FechaCheckin,
    [Range(1, int.MaxValue)] int EstadoCheckinId,
    [Required][StringLength(20, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Solo letras y números, hasta 20 caracteres.")]
    string NumeroTarjetaEmbarque,
    bool EquipajeBodega,
    [Range(0, 100)] decimal? PesoEquipajeKg);

public sealed record UpdateCheckInRequest(
    [Range(1, int.MaxValue)] int EstadoCheckinId,
    bool EquipajeBodega,
    [Range(0, 100)] decimal? PesoEquipajeKg);

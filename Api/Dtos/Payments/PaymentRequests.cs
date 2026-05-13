using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Payments;

public sealed record CreatePaymentMethodRequest(
    [Required][Range(1, int.MaxValue)] int TipoMedioPagoId,
    [Required][MaxLength(100)] string NombreComercial,
    int? TipoTarjetaId,
    int? EmisorTarjetaId);

public sealed record UpdatePaymentMethodRequest(
    [Required][MaxLength(100)] string NombreComercial,
    int? TipoTarjetaId,
    int? EmisorTarjetaId);

public sealed record CreatePaymentRequest(
    [Required][Range(1, int.MaxValue)] int ReservaId,
    [Required] decimal Monto,
    [Required] DateTime FechaPago,
    [Required][Range(1, int.MaxValue)] int EstadoPagoId,
    [Required][Range(1, int.MaxValue)] int MetodoPagoId);

public sealed record UpdatePaymentStatusRequest(
    [Required][Range(1, int.MaxValue)] int EstadoPagoId);

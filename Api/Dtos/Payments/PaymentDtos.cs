namespace Api.Dtos.Payments;

public sealed record PaymentMethodDto(
    int Id,
    int TipoMedioPagoId,
    string? NombreTipoMedio,
    string NombreComercial,
    int? TipoTarjetaId,
    string? NombreTipoTarjeta,
    int? EmisorTarjetaId,
    string? NombreEmisor);

public sealed record PaymentDto(
    int Id,
    int ReservaId,
    decimal Monto,
    DateTime FechaPago,
    int EstadoPagoId,
    string? NombreEstado,
    int MetodoPagoId,
    string? NombreMetodoPago,
    DateTime ActualizadoEn);

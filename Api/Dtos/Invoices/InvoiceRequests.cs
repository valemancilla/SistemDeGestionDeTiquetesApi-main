using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Invoices;

public sealed record CreateInvoiceRequest(
    [Range(1, int.MaxValue)] int ReservaId,
    [Required][StringLength(30, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Número de factura: 1 a 30 caracteres (letras, números o guion).")]
    string NumeroFactura,
    [Range(0, double.MaxValue)] decimal Subtotal,
    [Range(0, double.MaxValue)] decimal Impuestos);

public sealed record UpdateInvoiceRequest(
    [Range(0, double.MaxValue)] decimal Subtotal,
    [Range(0, double.MaxValue)] decimal Impuestos);

public sealed record CreateInvoiceItemRequest(
    [Range(1, int.MaxValue)] int FacturaId,
    [Range(1, int.MaxValue)] int TipoItemId,
    [Required][MaxLength(300)] string Descripcion,
    [Range(1, int.MaxValue)] int Cantidad,
    [Range(0, double.MaxValue)] decimal PrecioUnitario,
    int? ReservaPasajeroId);

public sealed record UpdateInvoiceItemRequest(
    [Range(1, int.MaxValue)] int Cantidad,
    [Range(0, double.MaxValue)] decimal PrecioUnitario,
    [Required][MaxLength(300)] string Descripcion);

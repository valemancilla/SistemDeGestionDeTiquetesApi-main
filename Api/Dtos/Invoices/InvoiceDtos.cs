namespace Api.Dtos.Invoices;

public sealed record InvoiceDto(
    int Id,
    int ReservaId,
    string NumeroFactura,
    DateTime FechaEmision,
    decimal Subtotal,
    decimal Impuestos,
    decimal Total);

public sealed record InvoiceItemDto(
    int Id,
    int FacturaId,
    int TipoItemId,
    string? NombreTipoItem,
    string Descripcion,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Subtotal,
    int? ReservaPasajeroId);

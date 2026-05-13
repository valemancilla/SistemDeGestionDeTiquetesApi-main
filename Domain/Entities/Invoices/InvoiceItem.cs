using Domain.Common;
using Domain.Entities.Reservations;

namespace Domain.Entities.Invoices;

public sealed class InvoiceItem : BaseEntity<int>
{
    public int FacturaId { get; private set; }
    public int TipoItemId { get; private set; }
    public string Descripcion { get; private set; } = default!;
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public decimal Subtotal { get; private set; }
    public int? ReservaPasajeroId { get; private set; }

    public Invoice? Factura { get; private set; }
    public InvoiceItemType? TipoItem { get; private set; }
    public ReservationPassenger? ReservaPasajero { get; private set; }

    private InvoiceItem() { }

    public InvoiceItem(int facturaId, int tipoItemId, string descripcion,
        int cantidad, decimal precioUnitario, int? reservaPasajeroId = null)
    {
        FacturaId = facturaId;
        TipoItemId = tipoItemId;
        Descripcion = descripcion;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        Subtotal = cantidad * precioUnitario;
        ReservaPasajeroId = reservaPasajeroId;
    }

    public void Update(int cantidad, decimal precioUnitario, string descripcion)
    {
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        Subtotal = cantidad * precioUnitario;
        Descripcion = descripcion;
    }
}

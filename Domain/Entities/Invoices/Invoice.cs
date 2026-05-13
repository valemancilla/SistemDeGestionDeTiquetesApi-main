using Domain.Common;
using Domain.Entities.Reservations;
using Domain.ValueObjects.Invoices;

namespace Domain.Entities.Invoices;

public sealed class Invoice : BaseEntity<int>
{
    public int ReservaId { get; private set; }
    public InvoiceNumber NumeroFactura { get; private set; } = default!;
    public DateTime FechaEmision { get; private set; } = DateTime.UtcNow;
    public decimal Subtotal { get; private set; }
    public decimal Impuestos { get; private set; }
    public decimal Total { get; private set; }

    public Reservation? Reserva { get; private set; }

    private Invoice() { }

    public Invoice(int reservaId, string numeroFactura, decimal subtotal, decimal impuestos)
    {
        ReservaId = reservaId;
        NumeroFactura = InvoiceNumber.Create(numeroFactura);
        Subtotal = subtotal;
        Impuestos = impuestos;
        Total = subtotal + impuestos;
    }

    public void RecalcularTotal(decimal subtotal, decimal impuestos)
    {
        Subtotal = subtotal;
        Impuestos = impuestos;
        Total = subtotal + impuestos;
    }
}

using Domain.Common;
using Domain.Entities.Reservations;

namespace Domain.Entities.Payments;

public sealed class Payment : BaseEntity<int>
{
    public int ReservaId { get; private set; }
    public decimal Monto { get; private set; }
    public DateTime FechaPago { get; private set; }
    public int EstadoPagoId { get; private set; }
    public int MetodoPagoId { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Reservation? Reserva { get; private set; }
    public PaymentState? EstadoPago { get; private set; }
    public PaymentMethod? MetodoPago { get; private set; }

    private Payment() { }

    public Payment(int reservaId, decimal monto, DateTime fechaPago, int estadoPagoId, int metodoPagoId)
    {
        ReservaId = reservaId;
        Monto = monto;
        FechaPago = fechaPago;
        EstadoPagoId = estadoPagoId;
        MetodoPagoId = metodoPagoId;
    }

    public void CambiarEstado(int nuevoEstadoId)
    {
        EstadoPagoId = nuevoEstadoId;
        ActualizadoEn = DateTime.UtcNow;
    }
}

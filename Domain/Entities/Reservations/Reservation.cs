using Domain.Common;
using Domain.Entities.Clients;
using Domain.ValueObjects.Reservations;

namespace Domain.Entities.Reservations;

public sealed class Reservation : BaseEntity<int>
{
    public BookingCode CodigoReserva { get; private set; } = default!;
    public int ClienteId { get; private set; }
    public DateTime FechaReserva { get; private set; } = DateTime.UtcNow;
    public int EstadoReservaId { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime? VenceEn { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Client? Cliente { get; private set; }
    public ReservationStatus? EstadoReserva { get; private set; }

    private Reservation() { }

    public Reservation(string codigoReserva, int clienteId, int estadoReservaId,
        decimal valorTotal, DateTime? venceEn = null)
    {
        CodigoReserva = BookingCode.Create(codigoReserva);
        ClienteId = clienteId;
        EstadoReservaId = estadoReservaId;
        ValorTotal = valorTotal;
        VenceEn = venceEn;
    }

    public void Update(decimal valorTotal, DateTime? venceEn)
    {
        ValorTotal = valorTotal;
        VenceEn = venceEn;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void CambiarEstado(int nuevoEstadoId)
    {
        EstadoReservaId = nuevoEstadoId;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void ActualizarValor(decimal nuevoValor)
    {
        ValorTotal = nuevoValor;
        ActualizadoEn = DateTime.UtcNow;
    }
}

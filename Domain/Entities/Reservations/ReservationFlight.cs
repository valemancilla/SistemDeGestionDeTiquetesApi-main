using Domain.Common;
using Domain.Entities.Flights;

namespace Domain.Entities.Reservations;

public sealed class ReservationFlight : BaseEntity<int>
{
    public int ReservaId { get; private set; }
    public int VueloId { get; private set; }
    public decimal ValorParcial { get; private set; }

    public Reservation? Reserva { get; private set; }
    public Flight? Vuelo { get; private set; }

    private ReservationFlight() { }

    public ReservationFlight(int reservaId, int vueloId, decimal valorParcial = 0)
    {
        ReservaId = reservaId;
        VueloId = vueloId;
        ValorParcial = valorParcial;
    }

    public void Update(decimal valorParcial) => ValorParcial = valorParcial;
}

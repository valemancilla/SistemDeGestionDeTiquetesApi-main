using Domain.Common;
using Domain.Entities.Passengers;

namespace Domain.Entities.Reservations;

public sealed class ReservationPassenger : BaseEntity<int>
{
    public int ReservaVueloId { get; private set; }
    public int PasajeroId { get; private set; }

    public ReservationFlight? ReservaVuelo { get; private set; }
    public Passenger? Pasajero { get; private set; }

    private ReservationPassenger() { }

    public ReservationPassenger(int reservaVueloId, int pasajeroId)
    {
        ReservaVueloId = reservaVueloId;
        PasajeroId = pasajeroId;
    }
}

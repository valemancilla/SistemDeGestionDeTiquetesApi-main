using Domain.Common;

namespace Domain.Entities.Reservations;

public sealed class ReservationStatus : LookupEntity
{
    private ReservationStatus() { }

    public ReservationStatus(string nombre) => Nombre = nombre;
}

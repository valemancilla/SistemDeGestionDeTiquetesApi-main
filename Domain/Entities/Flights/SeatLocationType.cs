using Domain.Common;

namespace Domain.Entities.Flights;

public sealed class SeatLocationType : LookupEntity
{
    private SeatLocationType() { }

    public SeatLocationType(string nombre) => Nombre = nombre;
}

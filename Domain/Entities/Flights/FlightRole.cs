using Domain.Common;

namespace Domain.Entities.Flights;

public sealed class FlightRole : LookupEntity
{
    private FlightRole() { }

    public FlightRole(string nombre) => Nombre = nombre;
}

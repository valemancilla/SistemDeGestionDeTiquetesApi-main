using Domain.Common;

namespace Domain.Entities.Staff;

public sealed class AvailabilityStatus : LookupEntity
{
    private AvailabilityStatus() { }

    public AvailabilityStatus(string nombre) => Nombre = nombre;
}

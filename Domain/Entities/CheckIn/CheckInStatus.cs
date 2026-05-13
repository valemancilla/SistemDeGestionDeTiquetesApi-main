using Domain.Common;

namespace Domain.Entities.CheckIn;

public sealed class CheckInStatus : LookupEntity
{
    private CheckInStatus() { }

    public CheckInStatus(string nombre) => Nombre = nombre;
}

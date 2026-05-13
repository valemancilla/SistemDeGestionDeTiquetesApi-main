using Domain.Common;

namespace Domain.Entities.Staff;

public sealed class StaffRole : LookupEntity
{
    private StaffRole() { }

    public StaffRole(string nombre) => Nombre = nombre;
}

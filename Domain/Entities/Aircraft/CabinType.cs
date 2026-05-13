using Domain.Common;

namespace Domain.Entities.Aircraft;

public sealed class CabinType : LookupEntity
{
    private CabinType() { }

    public CabinType(string nombre) => Nombre = nombre;
}

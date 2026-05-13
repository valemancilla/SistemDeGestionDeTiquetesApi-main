using Domain.Common;

namespace Domain.Entities.Geography;

public sealed class Continent : LookupEntity
{
    private Continent() { }
    public Continent(string nombre) : base(nombre) { }
}

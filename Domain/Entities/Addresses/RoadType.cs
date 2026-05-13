using Domain.Common;

namespace Domain.Entities.Addresses;

public sealed class RoadType : LookupEntity
{
    private RoadType() { }
    public RoadType(string nombre) : base(nombre) { }
}

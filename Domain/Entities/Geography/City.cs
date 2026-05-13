using Domain.Common;

namespace Domain.Entities.Geography;

public sealed class City : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public int RegionId { get; private set; }

    public Region? Region { get; private set; }

    private City() { }

    public City(string nombre, int regionId)
    {
        Nombre = nombre;
        RegionId = regionId;
    }

    public void Update(string nombre) => Nombre = nombre;
}

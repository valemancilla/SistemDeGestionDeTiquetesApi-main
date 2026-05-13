using Domain.Common;
using Domain.Entities.Geography;

namespace Domain.Entities.Aircraft;

public sealed class AircraftManufacturer : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public int PaisId { get; private set; }
    public Country? Pais { get; private set; }

    private AircraftManufacturer() { }

    public AircraftManufacturer(string nombre, int paisId)
    {
        Nombre = nombre;
        PaisId = paisId;
    }

    public void Update(string nombre, int paisId)
    {
        Nombre = nombre;
        PaisId = paisId;
    }
}

using Domain.Common;

namespace Domain.Entities.Fares;

public sealed class PassengerType : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public int? EdadMin { get; private set; }
    public int? EdadMax { get; private set; }

    private PassengerType() { }

    public PassengerType(string nombre, int? edadMin = null, int? edadMax = null)
    {
        Nombre = nombre;
        EdadMin = edadMin;
        EdadMax = edadMax;
    }

    public void Update(string nombre, int? edadMin, int? edadMax)
    {
        Nombre = nombre;
        EdadMin = edadMin;
        EdadMax = edadMax;
    }
}

using Domain.Common;

namespace Domain.Entities.Flights;

public sealed class FlightState : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;

    private FlightState() { }

    public FlightState(string nombre)
    {
        Nombre = nombre;
    }

    public void Update(string nombre) => Nombre = nombre;
}

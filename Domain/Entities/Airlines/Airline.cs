using Domain.Common;
using Domain.Entities.Geography;
using Domain.ValueObjects.Aviation;

namespace Domain.Entities.Airlines;

public sealed class Airline : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public IataCode CodigoIata { get; private set; } = default!;
    public int PaisOrigenId { get; private set; }
    public bool Activa { get; private set; } = true;
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Country? PaisOrigen { get; private set; }

    private Airline() { }

    public Airline(string nombre, string codigoIata, int paisOrigenId)
    {
        Nombre = nombre;
        CodigoIata = IataCode.Create(codigoIata);
        PaisOrigenId = paisOrigenId;
    }

    public void Update(string nombre, bool activa)
    {
        Nombre = nombre;
        Activa = activa;
        ActualizadoEn = DateTime.UtcNow;
    }
}

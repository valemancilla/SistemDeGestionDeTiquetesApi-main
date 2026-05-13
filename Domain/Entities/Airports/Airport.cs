using Domain.Common;
using Domain.Entities.Geography;
using Domain.ValueObjects.Aviation;

namespace Domain.Entities.Airports;

public sealed class Airport : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public IataCode CodigoIata { get; private set; } = default!;
    public IcaoCode? CodigoIcao { get; private set; }
    public int CiudadId { get; private set; }

    public City? Ciudad { get; private set; }

    private Airport() { }

    public Airport(string nombre, string codigoIata, int ciudadId, string? codigoIcao = null)
    {
        Nombre = nombre;
        CodigoIata = IataCode.Create(codigoIata);
        CodigoIcao = string.IsNullOrWhiteSpace(codigoIcao) ? null : IcaoCode.Create(codigoIcao);
        CiudadId = ciudadId;
    }

    public void Update(string nombre, string? codigoIcao)
    {
        Nombre = nombre;
        CodigoIcao = string.IsNullOrWhiteSpace(codigoIcao) ? null : IcaoCode.Create(codigoIcao);
    }
}

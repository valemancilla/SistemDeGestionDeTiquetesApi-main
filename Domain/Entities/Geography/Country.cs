using Domain.Common;
using Domain.ValueObjects.Geography;

namespace Domain.Entities.Geography;

public sealed class Country : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public IsoCountryCode CodigoIso { get; private set; } = default!;
    public int ContinenteId { get; private set; }

    public Continent? Continente { get; private set; }

    private Country() { }

    public Country(string nombre, string codigoIso, int continenteId)
    {
        Nombre = nombre;
        CodigoIso = IsoCountryCode.Create(codigoIso);
        ContinenteId = continenteId;
    }

    public void Update(string nombre, string codigoIso)
    {
        Nombre = nombre;
        CodigoIso = IsoCountryCode.Create(codigoIso);
    }
}

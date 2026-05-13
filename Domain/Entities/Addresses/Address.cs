using Domain.Common;
using Domain.Entities.Geography;
using Domain.ValueObjects.Addresses;

namespace Domain.Entities.Addresses;

public sealed class Address : BaseEntity<int>
{
    public int TipoViaId { get; private set; }
    public string NombreVia { get; private set; } = default!;
    public string? Numero { get; private set; }
    public string? Complemento { get; private set; }
    public int CiudadId { get; private set; }
    public PostalCode? CodigoPostal { get; private set; }

    public RoadType? TipoVia { get; private set; }
    public City? Ciudad { get; private set; }

    private Address() { }

    public Address(int tipoViaId, string nombreVia, int ciudadId,
        string? numero = null, string? complemento = null, string? codigoPostal = null)
    {
        TipoViaId = tipoViaId;
        NombreVia = nombreVia;
        CiudadId = ciudadId;
        Numero = numero;
        Complemento = complemento;
        CodigoPostal = PostalCode.CreateOptional(codigoPostal);
    }

    public void Update(int tipoViaId, string nombreVia, int ciudadId,
        string? numero, string? complemento, string? codigoPostal)
    {
        TipoViaId = tipoViaId;
        NombreVia = nombreVia;
        CiudadId = ciudadId;
        Numero = numero;
        Complemento = complemento;
        CodigoPostal = PostalCode.CreateOptional(codigoPostal);
    }
}

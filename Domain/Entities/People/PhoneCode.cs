using Domain.Common;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class PhoneCode : BaseEntity<int>
{
    public PhoneCountryCode CodigoPais { get; private set; } = default!;
    public string NombrePais { get; private set; } = default!;

    private PhoneCode() { }

    public PhoneCode(string codigoPais, string nombrePais)
    {
        CodigoPais = PhoneCountryCode.Create(codigoPais);
        NombrePais = nombrePais;
    }

    public void Update(string codigoPais, string nombrePais)
    {
        CodigoPais = PhoneCountryCode.Create(codigoPais);
        NombrePais = nombrePais;
    }
}

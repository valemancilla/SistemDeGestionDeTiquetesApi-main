using Domain.Common;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class PersonPhone : BaseEntity<int>
{
    public int PersonaId { get; private set; }
    public int CodigoTelefonoId { get; private set; }
    public PhoneNationalNumber NumeroTelefono { get; private set; } = default!;
    public bool EsPrincipal { get; private set; }

    public Person? Persona { get; private set; }
    public PhoneCode? CodigoTelefono { get; private set; }

    private PersonPhone() { }

    public PersonPhone(int personaId, int codigoTelefonoId, string numeroTelefono, bool esPrincipal = false)
    {
        PersonaId = personaId;
        CodigoTelefonoId = codigoTelefonoId;
        NumeroTelefono = PhoneNationalNumber.Create(numeroTelefono);
        EsPrincipal = esPrincipal;
    }

    public void Update(int codigoTelefonoId, string numeroTelefono, bool esPrincipal)
    {
        CodigoTelefonoId = codigoTelefonoId;
        NumeroTelefono = PhoneNationalNumber.Create(numeroTelefono);
        EsPrincipal = esPrincipal;
    }
}

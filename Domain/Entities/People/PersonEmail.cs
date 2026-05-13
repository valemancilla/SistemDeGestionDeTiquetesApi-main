using Domain.Common;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class PersonEmail : BaseEntity<int>
{
    public int PersonaId { get; private set; }
    public EmailLocalPart UsuarioEmail { get; private set; } = default!;
    public int DominioEmailId { get; private set; }
    public bool EsPrincipal { get; private set; }

    public Person? Persona { get; private set; }
    public EmailDomain? DominioEmail { get; private set; }

    private PersonEmail() { }

    public PersonEmail(int personaId, string usuarioEmail, int dominioEmailId, bool esPrincipal = false)
    {
        PersonaId = personaId;
        UsuarioEmail = EmailLocalPart.Create(usuarioEmail);
        DominioEmailId = dominioEmailId;
        EsPrincipal = esPrincipal;
    }

    public void Update(string usuarioEmail, int dominioEmailId, bool esPrincipal)
    {
        UsuarioEmail = EmailLocalPart.Create(usuarioEmail);
        DominioEmailId = dominioEmailId;
        EsPrincipal = esPrincipal;
    }
}

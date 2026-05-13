using Domain.Common;
using Domain.ValueObjects.Auth;

namespace Domain.Entities.Auth;

public sealed class Session : BaseEntity<int>
{
    public int UsuarioId { get; private set; }
    public DateTime IniciadaEn { get; private set; } = DateTime.UtcNow;
    public DateTime? CerradaEn { get; private set; }
    public SessionIpAddress? IpOrigen { get; private set; }
    public bool Activa { get; private set; } = true;

    public AppUser? Usuario { get; private set; }

    private Session() { }

    public Session(int usuarioId, string? ipOrigen = null)
    {
        UsuarioId = usuarioId;
        IpOrigen = SessionIpAddress.CreateOptional(ipOrigen);
    }

    public void Cerrar()
    {
        CerradaEn = DateTime.UtcNow;
        Activa = false;
    }
}

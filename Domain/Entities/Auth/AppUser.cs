using Domain.Common;
using Domain.Entities.People;
using Domain.ValueObjects.Auth;

namespace Domain.Entities.Auth;

public sealed class AppUser : BaseEntity<int>
{
    public Username Username { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public int? PersonaId { get; private set; }
    public int RolId { get; private set; }
    public bool Activo { get; private set; } = true;
    public DateTime? UltimoAcceso { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Person? Persona { get; private set; }
    public SystemRole? Rol { get; private set; }

    private AppUser() { }

    public AppUser(string username, string passwordHash, int rolId, int? personaId = null)
    {
        Username = global::Domain.ValueObjects.Auth.Username.Create(username);
        PasswordHash = passwordHash;
        RolId = rolId;
        PersonaId = personaId;
    }

    public void RegistrarAcceso()
    {
        UltimoAcceso = DateTime.UtcNow;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void Update(int rolId, bool activo)
    {
        RolId = rolId;
        Activo = activo;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void CambiarPassword(string nuevoHash)
    {
        PasswordHash = nuevoHash;
        ActualizadoEn = DateTime.UtcNow;
    }
}

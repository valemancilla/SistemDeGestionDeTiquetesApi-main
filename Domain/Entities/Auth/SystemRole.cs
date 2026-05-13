using Domain.Common;

namespace Domain.Entities.Auth;

public sealed class SystemRole : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public string? Descripcion { get; private set; }

    private SystemRole() { }

    public SystemRole(string nombre, string? descripcion = null)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public void Update(string nombre, string? descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
}

using Domain.Common;

namespace Domain.Entities.Auth;

public sealed class RolePermission : BaseEntity<int>
{
    public int RolId { get; private set; }
    public int PermisoId { get; private set; }

    public SystemRole? Rol { get; private set; }
    public Permission? Permiso { get; private set; }

    private RolePermission() { }

    public RolePermission(int rolId, int permisoId)
    {
        RolId = rolId;
        PermisoId = permisoId;
    }
}

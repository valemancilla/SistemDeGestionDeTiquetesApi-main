using Api.Dtos.Auth;
using Domain.Entities.Auth;
using Mapster;

namespace Api.Mappings;

public sealed class AuthMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SystemRole, SystemRoleDto>();

        config.NewConfig<Permission, PermissionDto>();

        config.NewConfig<RolePermission, RolePermissionDto>()
            .Map(d => d.NombreRol, s => s.Rol != null ? s.Rol.Nombre : null)
            .Map(d => d.NombrePermiso, s => s.Permiso != null ? s.Permiso.Nombre : null);

        config.NewConfig<AppUser, AppUserDto>()
            .Map(d => d.Username, s => s.Username.Value)
            .Map(d => d.NombreRol, s => s.Rol != null ? s.Rol.Nombre : null);

        config.NewConfig<Session, SessionDto>()
            .Map(d => d.Username, s => s.Usuario != null ? s.Usuario.Username.Value : null)
            .Map(d => d.IpOrigen, s => s.IpOrigen != null ? s.IpOrigen.Value : null);
    }
}

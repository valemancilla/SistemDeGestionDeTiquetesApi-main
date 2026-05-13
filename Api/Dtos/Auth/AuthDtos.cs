namespace Api.Dtos.Auth;

public sealed record SystemRoleDto(int Id, string Nombre, string? Descripcion);

public sealed record PermissionDto(int Id, string Nombre, string? Descripcion);

public sealed record RolePermissionDto(int Id, int RolId, string? NombreRol, int PermisoId, string? NombrePermiso);

public sealed record AppUserDto(
    int Id,
    string Username,
    int RolId,
    string? NombreRol,
    int? PersonaId,
    bool Activo,
    DateTime? UltimoAcceso,
    DateTime ActualizadoEn);

public sealed record SessionDto(
    int Id,
    int UsuarioId,
    string? Username,
    DateTime IniciadaEn,
    DateTime? CerradaEn,
    string? IpOrigen,
    bool Activa);

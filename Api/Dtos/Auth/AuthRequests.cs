using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Auth;

public sealed record CreateSystemRoleRequest(
    [Required][MaxLength(100)] string Nombre,
    [MaxLength(300)] string? Descripcion);

public sealed record CreatePermissionRequest(
    [Required][MaxLength(100)] string Nombre,
    [MaxLength(300)] string? Descripcion);

public sealed record CreateRolePermissionRequest(
    [Required][Range(1, int.MaxValue)] int RolId,
    [Required][Range(1, int.MaxValue)] int PermisoId);

public sealed record CreateAppUserRequest(
    [Required][StringLength(50, MinimumLength = 3)][RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Usuario: 3 a 50 caracteres. Puede usar mayúsculas, minúsculas o ambas, y números, punto, guion o guion bajo.")]
    string Username,
    [Required][MaxLength(256)] string PasswordHash,
    [Required][Range(1, int.MaxValue)] int RolId,
    int? PersonaId);

public sealed record UpdateAppUserRequest(
    [Required][Range(1, int.MaxValue)] int RolId,
    bool Activo);

public sealed record ChangePasswordRequest(
    [Required][MaxLength(256)] string PasswordHash);

public sealed record CreateSessionRequest(
    [Required][Range(1, int.MaxValue)] int UsuarioId,
    [MaxLength(45)] string? IpOrigen);

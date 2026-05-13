namespace Api.Dtos.People;

public sealed record PersonEmailDto(
    int Id,
    int PersonaId,
    string UsuarioEmail,
    int DominioEmailId,
    string? Dominio,
    bool EsPrincipal);

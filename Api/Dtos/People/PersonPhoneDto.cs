namespace Api.Dtos.People;

public sealed record PersonPhoneDto(
    int Id,
    int PersonaId,
    int CodigoTelefonoId,
    string? CodigoPais,
    string NumeroTelefono,
    bool EsPrincipal);

namespace Api.Dtos.Clients;

public sealed record ClientDto(
    int Id,
    int PersonaId,
    string? Nombres,
    string? Apellidos);

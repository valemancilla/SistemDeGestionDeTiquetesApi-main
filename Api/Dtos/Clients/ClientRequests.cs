using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Clients;

public sealed record CreateClientRequest(
    [Range(1, int.MaxValue)] int PersonaId);

using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Passengers;

public sealed record CreatePassengerRequest(
    [Range(1, int.MaxValue)] int PersonaId,
    [Range(1, int.MaxValue)] int TipoPasajeroId);

public sealed record UpdatePassengerRequest(
    [Range(1, int.MaxValue)] int TipoPasajeroId);

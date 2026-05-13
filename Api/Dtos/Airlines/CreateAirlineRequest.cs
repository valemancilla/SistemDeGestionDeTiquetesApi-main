using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Airlines;

public sealed record CreateAirlineRequest(
    [Required][MaxLength(150)] string Nombre,
    [Required][StringLength(3, MinimumLength = 3)][RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "El código IATA debe ser exactamente 3 letras.")]
    string CodigoIata,
    [Range(1, int.MaxValue)] int PaisOrigenId);

public sealed record UpdateAirlineRequest(
    [Required][MaxLength(150)] string Nombre,
    bool Activa);

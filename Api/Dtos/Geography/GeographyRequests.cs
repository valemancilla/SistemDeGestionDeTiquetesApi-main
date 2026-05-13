using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Geography;

public sealed record CreateCountryRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][StringLength(3, MinimumLength = 2)][RegularExpression(@"^[A-Za-z]{2,3}$", ErrorMessage = "El código ISO debe tener 2 o 3 letras.")]
    string CodigoIso,
    [Range(1, int.MaxValue)] int ContinenteId);

public sealed record UpdateCountryRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][StringLength(3, MinimumLength = 2)][RegularExpression(@"^[A-Za-z]{2,3}$", ErrorMessage = "El código ISO debe tener 2 o 3 letras.")]
    string CodigoIso);

public sealed record CreateRegionRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][MaxLength(50)] string Tipo,
    [Range(1, int.MaxValue)] int PaisId);

public sealed record UpdateRegionRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][MaxLength(50)] string Tipo);

public sealed record CreateCityRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(1, int.MaxValue)] int RegionId);

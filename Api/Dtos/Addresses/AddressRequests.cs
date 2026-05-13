using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Addresses;

public sealed record CreateAddressRequest(
    [Range(1, int.MaxValue)] int TipoViaId,
    [Required][MaxLength(150)] string NombreVia,
    [Range(1, int.MaxValue)] int CiudadId,
    [MaxLength(20)] string? Numero,
    [MaxLength(50)] string? Complemento,
    [RegularExpression(@"^$|^[A-Za-z0-9 \-]{1,20}$", ErrorMessage = "Código postal: hasta 20 letras, números, espacio o guion.")]
    string? CodigoPostal);

public sealed record UpdateAddressRequest(
    [Range(1, int.MaxValue)] int TipoViaId,
    [Required][MaxLength(150)] string NombreVia,
    [Range(1, int.MaxValue)] int CiudadId,
    [MaxLength(20)] string? Numero,
    [MaxLength(50)] string? Complemento,
    [RegularExpression(@"^$|^[A-Za-z0-9 \-]{1,20}$", ErrorMessage = "Código postal: hasta 20 letras, números, espacio o guion.")]
    string? CodigoPostal);

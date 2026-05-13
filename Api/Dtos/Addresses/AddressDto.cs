namespace Api.Dtos.Addresses;

public sealed record AddressDto(
    int Id,
    int TipoViaId,
    string? NombreTipoVia,
    string NombreVia,
    string? Numero,
    string? Complemento,
    int CiudadId,
    string? NombreCiudad,
    string? CodigoPostal);

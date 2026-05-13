namespace Api.Dtos.People;

public sealed record PersonDto(
    int Id,
    int TipoDocumentoId,
    string? NombreTipoDocumento,
    string NumeroDocumento,
    string Nombres,
    string Apellidos,
    DateOnly? FechaNacimiento,
    string? Genero,
    int? DireccionId);

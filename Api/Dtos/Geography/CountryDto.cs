namespace Api.Dtos.Geography;

public sealed record CountryDto(
    int Id,
    string Nombre,
    string CodigoIso,
    int ContinenteId,
    string? NombreContinente);

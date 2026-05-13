namespace Api.Dtos.Geography;

public sealed record RegionDto(
    int Id,
    string Nombre,
    string Tipo,
    int PaisId,
    string? NombrePais);

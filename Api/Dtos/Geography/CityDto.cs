namespace Api.Dtos.Geography;

public sealed record CityDto(
    int Id,
    string Nombre,
    int RegionId,
    string? NombreRegion);

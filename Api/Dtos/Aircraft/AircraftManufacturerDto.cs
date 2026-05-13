namespace Api.Dtos.Aircraft;

public sealed record AircraftManufacturerDto(int Id, string Nombre, int PaisId, string? NombrePais);

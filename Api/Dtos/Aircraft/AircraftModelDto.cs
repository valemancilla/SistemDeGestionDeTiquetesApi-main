namespace Api.Dtos.Aircraft;

public sealed record AircraftModelDto(
    int Id,
    int FabricanteId,
    string? NombreFabricante,
    string NombreModelo,
    int CapacidadMaxima,
    decimal? PesoMaxDespegueKg,
    decimal? ConsumoCombustibleKgH,
    int? VelocidadCruceroKmh,
    int? AltitudCruceroFt);

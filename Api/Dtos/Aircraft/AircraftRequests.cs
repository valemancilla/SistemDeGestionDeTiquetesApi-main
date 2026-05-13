using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Aircraft;

public sealed record CreateAircraftManufacturerRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(1, int.MaxValue)] int PaisId);

public sealed record UpdateAircraftManufacturerRequest(
    [Required][MaxLength(100)] string Nombre,
    [Range(1, int.MaxValue)] int PaisId);

public sealed record CreateAircraftModelRequest(
    [Range(1, int.MaxValue)] int FabricanteId,
    [Required][MaxLength(100)] string NombreModelo,
    [Range(1, int.MaxValue)] int CapacidadMaxima,
    decimal? PesoMaxDespegueKg,
    decimal? ConsumoCombustibleKgH,
    int? VelocidadCruceroKmh,
    int? AltitudCruceroFt);

public sealed record UpdateAircraftModelRequest(
    [Required][MaxLength(100)] string NombreModelo,
    [Range(1, int.MaxValue)] int CapacidadMaxima,
    decimal? PesoMaxDespegueKg,
    decimal? ConsumoCombustibleKgH,
    int? VelocidadCruceroKmh,
    int? AltitudCruceroFt);

public sealed record CreateAircraftRequest(
    [Range(1, int.MaxValue)] int ModeloId,
    [Range(1, int.MaxValue)] int AerolineaId,
    [Required][StringLength(20, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Matrícula: 1 a 20 caracteres (letras, números o guion).")]
    string Matricula,
    DateOnly? FechaFabricacion);

public sealed record UpdateAircraftRequest(
    [Range(1, int.MaxValue)] int ModeloId,
    [Range(1, int.MaxValue)] int AerolineaId,
    DateOnly? FechaFabricacion,
    bool Activa);

using Domain.Common;

namespace Domain.Entities.Aircraft;

public sealed class AircraftModel : BaseEntity<int>
{
    public int FabricanteId { get; private set; }
    public string NombreModelo { get; private set; } = default!;
    public int CapacidadMaxima { get; private set; }
    public decimal? PesoMaxDespegueKg { get; private set; }
    public decimal? ConsumoCombustibleKgH { get; private set; }
    public int? VelocidadCruceroKmh { get; private set; }
    public int? AltitudCruceroFt { get; private set; }

    public AircraftManufacturer? Fabricante { get; private set; }

    private AircraftModel() { }

    public AircraftModel(int fabricanteId, string nombreModelo, int capacidadMaxima,
        decimal? pesoMaxDespegueKg = null, decimal? consumoCombustibleKgH = null,
        int? velocidadCruceroKmh = null, int? altitudCruceroFt = null)
    {
        FabricanteId = fabricanteId;
        NombreModelo = nombreModelo;
        CapacidadMaxima = capacidadMaxima;
        PesoMaxDespegueKg = pesoMaxDespegueKg;
        ConsumoCombustibleKgH = consumoCombustibleKgH;
        VelocidadCruceroKmh = velocidadCruceroKmh;
        AltitudCruceroFt = altitudCruceroFt;
    }

    public void Update(string nombreModelo, int capacidadMaxima,
        decimal? pesoMaxDespegueKg, decimal? consumoCombustibleKgH,
        int? velocidadCruceroKmh, int? altitudCruceroFt)
    {
        NombreModelo = nombreModelo;
        CapacidadMaxima = capacidadMaxima;
        PesoMaxDespegueKg = pesoMaxDespegueKg;
        ConsumoCombustibleKgH = consumoCombustibleKgH;
        VelocidadCruceroKmh = velocidadCruceroKmh;
        AltitudCruceroFt = altitudCruceroFt;
    }
}

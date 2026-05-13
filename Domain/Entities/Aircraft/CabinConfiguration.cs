using Domain.Common;
using Domain.ValueObjects.Aircraft;

namespace Domain.Entities.Aircraft;

public sealed class CabinConfiguration : BaseEntity<int>
{
    public int AeronaveId { get; private set; }
    public int TipoCabinaId { get; private set; }
    public int FilaInicio { get; private set; }
    public int FilaFin { get; private set; }
    public int AsientosPorFila { get; private set; }
    public CabinSeatLetters LetrasAsientos { get; private set; } = default!;

    public Aircraft? Aeronave { get; private set; }
    public CabinType? TipoCabina { get; private set; }

    private CabinConfiguration() { }

    public CabinConfiguration(int aeronaveId, int tipoCabinaId, int filaInicio, int filaFin,
        int asientosPorFila, string letrasAsientos)
    {
        AeronaveId = aeronaveId;
        TipoCabinaId = tipoCabinaId;
        FilaInicio = filaInicio;
        FilaFin = filaFin;
        AsientosPorFila = asientosPorFila;
        LetrasAsientos = CabinSeatLetters.Create(letrasAsientos);
    }

    public void Update(int filaInicio, int filaFin, int asientosPorFila, string letrasAsientos)
    {
        FilaInicio = filaInicio;
        FilaFin = filaFin;
        AsientosPorFila = asientosPorFila;
        LetrasAsientos = CabinSeatLetters.Create(letrasAsientos);
    }
}

using Domain.Common;
using Domain.Entities.Aircraft;

namespace Domain.Entities.Maintenance;

public sealed class AircraftMaintenance : BaseEntity<int>
{
    public int AeronaveId { get; private set; }
    public int TipoMantenimientoId { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime? FechaFin { get; private set; }
    public string? Descripcion { get; private set; }

    public Aircraft.Aircraft? Aeronave { get; private set; }
    public MaintenanceType? TipoMantenimiento { get; private set; }

    private AircraftMaintenance() { }

    public AircraftMaintenance(int aeronaveId, int tipoMantenimientoId,
        DateTime fechaInicio, string? descripcion = null)
    {
        AeronaveId = aeronaveId;
        TipoMantenimientoId = tipoMantenimientoId;
        FechaInicio = fechaInicio;
        Descripcion = descripcion;
    }

    public void Update(int tipoMantenimientoId, DateTime fechaInicio, DateTime? fechaFin, string? descripcion)
    {
        TipoMantenimientoId = tipoMantenimientoId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Descripcion = descripcion;
    }

    public void Finalizar(DateTime fechaFin) => FechaFin = fechaFin;
}

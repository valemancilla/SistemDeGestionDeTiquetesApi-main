using Domain.Common;

namespace Domain.Entities.Staff;

public sealed class StaffAvailability : BaseEntity<int>
{
    public int PersonalId { get; private set; }
    public int EstadoDisponibilidadId { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaFin { get; private set; }
    public string? Observacion { get; private set; }

    public Staff? Personal { get; private set; }
    public AvailabilityStatus? EstadoDisponibilidad { get; private set; }

    private StaffAvailability() { }

    public StaffAvailability(int personalId, int estadoDisponibilidadId,
        DateTime fechaInicio, DateTime fechaFin, string? observacion = null)
    {
        PersonalId = personalId;
        EstadoDisponibilidadId = estadoDisponibilidadId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Observacion = observacion;
    }

    public void Update(int estadoDisponibilidadId, DateTime fechaInicio, DateTime fechaFin, string? observacion)
    {
        EstadoDisponibilidadId = estadoDisponibilidadId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Observacion = observacion;
    }
}

using Domain.Common;
using Domain.Entities.Flights;
using Domain.Entities.Staff;
using Domain.Entities.Tickets;
using Domain.ValueObjects.CheckIn;

namespace Domain.Entities.CheckIn;

public sealed class CheckIn : BaseEntity<int>
{
    public int TiqueteId { get; private set; }
    public int PersonalId { get; private set; }
    public int AsientoVueloId { get; private set; }
    public DateTime FechaCheckin { get; private set; }
    public int EstadoCheckinId { get; private set; }
    public BoardingPassNumber NumeroTarjetaEmbarque { get; private set; } = default!;
    public bool EquipajeBodega { get; private set; }
    public decimal? PesoEquipajeKg { get; private set; }

    public Ticket? Tiquete { get; private set; }
    public Staff.Staff? Personal { get; private set; }
    public FlightSeat? AsientoVuelo { get; private set; }
    public CheckInStatus? EstadoCheckin { get; private set; }

    private CheckIn() { }

    public CheckIn(int tiqueteId, int personalId, int asientoVueloId, DateTime fechaCheckin,
        int estadoCheckinId, string numeroTarjetaEmbarque,
        bool equipajeBodega = false, decimal? pesoEquipajeKg = null)
    {
        TiqueteId = tiqueteId;
        PersonalId = personalId;
        AsientoVueloId = asientoVueloId;
        FechaCheckin = fechaCheckin;
        EstadoCheckinId = estadoCheckinId;
        NumeroTarjetaEmbarque = BoardingPassNumber.Create(numeroTarjetaEmbarque);
        EquipajeBodega = equipajeBodega;
        PesoEquipajeKg = pesoEquipajeKg;
    }

    public void Update(int estadoCheckinId, bool equipajeBodega, decimal? pesoEquipajeKg)
    {
        EstadoCheckinId = estadoCheckinId;
        EquipajeBodega = equipajeBodega;
        PesoEquipajeKg = pesoEquipajeKg;
    }
}

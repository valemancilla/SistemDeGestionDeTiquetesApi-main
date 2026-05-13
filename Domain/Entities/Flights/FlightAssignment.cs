using Domain.Common;
using Domain.Entities.Staff;

namespace Domain.Entities.Flights;

public sealed class FlightAssignment : BaseEntity<int>
{
    public int VueloId { get; private set; }
    public int PersonalId { get; private set; }
    public int RolVueloId { get; private set; }

    public Flight? Vuelo { get; private set; }
    public Staff.Staff? Personal { get; private set; }
    public FlightRole? RolVuelo { get; private set; }

    private FlightAssignment() { }

    public FlightAssignment(int vueloId, int personalId, int rolVueloId)
    {
        VueloId = vueloId;
        PersonalId = personalId;
        RolVueloId = rolVueloId;
    }

    public void Update(int rolVueloId) => RolVueloId = rolVueloId;
}

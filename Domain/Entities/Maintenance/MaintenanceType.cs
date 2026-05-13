using Domain.Common;

namespace Domain.Entities.Maintenance;

public sealed class MaintenanceType : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;

    private MaintenanceType() { }

    public MaintenanceType(string nombre)
    {
        Nombre = nombre;
    }

    public void Update(string nombre) => Nombre = nombre;
}

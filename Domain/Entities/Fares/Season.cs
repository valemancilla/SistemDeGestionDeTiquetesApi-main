using Domain.Common;

namespace Domain.Entities.Fares;

public sealed class Season : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public string? Descripcion { get; private set; }
    public decimal PrecioFactor { get; private set; } = 1.0000m;

    private Season() { }

    public Season(string nombre, decimal precioFactor, string? descripcion = null)
    {
        Nombre = nombre;
        PrecioFactor = precioFactor;
        Descripcion = descripcion;
    }

    public void Update(string nombre, decimal precioFactor, string? descripcion)
    {
        Nombre = nombre;
        PrecioFactor = precioFactor;
        Descripcion = descripcion;
    }
}

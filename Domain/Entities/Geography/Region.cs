using Domain.Common;

namespace Domain.Entities.Geography;

public sealed class Region : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public string Tipo { get; private set; } = default!;
    public int PaisId { get; private set; }

    public Country? Pais { get; private set; }

    private Region() { }

    public Region(string nombre, string tipo, int paisId)
    {
        Nombre = nombre;
        Tipo = tipo;
        PaisId = paisId;
    }

    public void Update(string nombre, string tipo)
    {
        Nombre = nombre;
        Tipo = tipo;
    }
}

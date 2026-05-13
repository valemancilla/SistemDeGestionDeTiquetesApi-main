namespace Domain.Common;

public abstract class LookupEntity : BaseEntity<int>
{
    public string Nombre { get; protected set; } = default!;
    protected LookupEntity() { }
    protected LookupEntity(string nombre) => Nombre = nombre;
    public void Update(string nombre) => Nombre = nombre;
}

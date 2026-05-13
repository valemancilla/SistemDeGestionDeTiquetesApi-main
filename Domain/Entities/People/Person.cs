using Domain.Common;
using Domain.Entities.Addresses;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class Person : BaseEntity<int>
{
    public int TipoDocumentoId { get; private set; }
    public DocumentNumber NumeroDocumento { get; private set; } = default!;
    public PersonName Nombres { get; private set; } = default!;
    public PersonName Apellidos { get; private set; } = default!;
    public DateOnly? FechaNacimiento { get; private set; }
    public char? Genero { get; private set; }
    public int? DireccionId { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public DocumentType? TipoDocumento { get; private set; }
    public Address? Direccion { get; private set; }

    private Person() { }

    public Person(int tipoDocumentoId, string numeroDocumento, string nombres, string apellidos,
        DateOnly? fechaNacimiento = null, char? genero = null, int? direccionId = null)
    {
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = DocumentNumber.Create(numeroDocumento);
        Nombres = PersonName.Create(nombres);
        Apellidos = PersonName.Create(apellidos);
        FechaNacimiento = fechaNacimiento;
        Genero = genero;
        DireccionId = direccionId;
    }

    public void Update(string nombres, string apellidos, char? genero, int? direccionId)
    {
        Nombres = PersonName.Create(nombres);
        Apellidos = PersonName.Create(apellidos);
        Genero = genero;
        DireccionId = direccionId;
        ActualizadoEn = DateTime.UtcNow;
    }
}

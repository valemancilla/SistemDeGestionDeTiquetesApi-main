using Domain.Common;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class DocumentType : BaseEntity<int>
{
    public string Nombre { get; private set; } = default!;
    public DocumentTypeCode Codigo { get; private set; } = default!;

    private DocumentType() { }

    public DocumentType(string nombre, string codigo)
    {
        Nombre = nombre;
        Codigo = DocumentTypeCode.Create(codigo);
    }

    public void Update(string nombre, string codigo)
    {
        Nombre = nombre;
        Codigo = DocumentTypeCode.Create(codigo);
    }
}

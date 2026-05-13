using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Código de tipo de documento (columna <c>Code</c>, hasta 255 caracteres).</summary>
public sealed class DocumentTypeCode : IEquatable<DocumentTypeCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9]{1,255}$", RegexOptions.Compiled);

    public string Value { get; }

    private DocumentTypeCode(string value) => Value = value;

    public static DocumentTypeCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de tipo de documento no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código debe tener entre 1 y 255 caracteres (A-Z, 0-9).", nameof(value));
        return new DocumentTypeCode(v);
    }

    public bool Equals(DocumentTypeCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DocumentTypeCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(DocumentTypeCode? a, DocumentTypeCode? b) => Equals(a, b);
    public static bool operator !=(DocumentTypeCode? a, DocumentTypeCode? b) => !Equals(a, b);
}

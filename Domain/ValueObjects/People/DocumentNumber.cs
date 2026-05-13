using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Número de documento de identidad (<c>document_number</c>, hasta 30 caracteres).</summary>
public sealed class DocumentNumber : IEquatable<DocumentNumber>
{
    private static readonly Regex Pattern = new("^[A-Z0-9\\-]{1,30}$", RegexOptions.Compiled);

    public string Value { get; }

    private DocumentNumber(string value) => Value = value;

    public static DocumentNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de documento no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El número de documento admite hasta 30 caracteres (A-Z, 0-9, guion).", nameof(value));
        return new DocumentNumber(v);
    }

    public bool Equals(DocumentNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DocumentNumber o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(DocumentNumber? a, DocumentNumber? b) => Equals(a, b);
    public static bool operator !=(DocumentNumber? a, DocumentNumber? b) => !Equals(a, b);
}

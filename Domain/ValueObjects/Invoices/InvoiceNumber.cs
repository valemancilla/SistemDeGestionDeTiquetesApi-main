using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Invoices;

/// <summary>Número de factura (<c>numero_factura</c>, hasta 30 caracteres).</summary>
public sealed class InvoiceNumber : IEquatable<InvoiceNumber>
{
    private static readonly Regex Pattern = new("^[A-Z0-9\\-]{1,30}$", RegexOptions.Compiled);

    public string Value { get; }

    private InvoiceNumber(string value) => Value = value;

    public static InvoiceNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de factura no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El número de factura debe tener entre 1 y 30 caracteres (A-Z, 0-9, guion).", nameof(value));
        return new InvoiceNumber(v);
    }

    public bool Equals(InvoiceNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is InvoiceNumber o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(InvoiceNumber? a, InvoiceNumber? b) => Equals(a, b);
    public static bool operator !=(InvoiceNumber? a, InvoiceNumber? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Reservations;

/// <summary>Código de reserva (<c>booking_code</c>, hasta 30 caracteres).</summary>
public sealed class BookingCode : IEquatable<BookingCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9]{1,30}$", RegexOptions.Compiled);

    public string Value { get; }

    private BookingCode(string value) => Value = value;

    public static BookingCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de reserva no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código de reserva debe tener entre 1 y 30 caracteres (A-Z, 0-9).", nameof(value));
        return new BookingCode(v);
    }

    public bool Equals(BookingCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is BookingCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(BookingCode? a, BookingCode? b) => Equals(a, b);
    public static bool operator !=(BookingCode? a, BookingCode? b) => !Equals(a, b);
}

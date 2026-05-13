using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aviation;

/// <summary>Código de asiento (<c>seat_code</c>, hasta 5 caracteres).</summary>
public sealed class SeatCode : IEquatable<SeatCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9]{1,5}$", RegexOptions.Compiled);

    public string Value { get; }

    private SeatCode(string value) => Value = value;

    public static SeatCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de asiento no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código de asiento debe tener entre 1 y 5 caracteres (A-Z, 0-9).", nameof(value));
        return new SeatCode(v);
    }

    public bool Equals(SeatCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SeatCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(SeatCode? a, SeatCode? b) => Equals(a, b);
    public static bool operator !=(SeatCode? a, SeatCode? b) => !Equals(a, b);
}

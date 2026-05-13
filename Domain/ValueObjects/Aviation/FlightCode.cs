using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aviation;

/// <summary>Código de vuelo (<c>flight_code</c>, hasta 10 caracteres).</summary>
public sealed class FlightCode : IEquatable<FlightCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9\\-]{1,10}$", RegexOptions.Compiled);

    public string Value { get; }

    private FlightCode(string value) => Value = value;

    public static FlightCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de vuelo no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código de vuelo debe tener entre 1 y 10 caracteres (A-Z, 0-9, guion).", nameof(value));
        return new FlightCode(v);
    }

    public bool Equals(FlightCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is FlightCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(FlightCode? a, FlightCode? b) => Equals(a, b);
    public static bool operator !=(FlightCode? a, FlightCode? b) => !Equals(a, b);
}

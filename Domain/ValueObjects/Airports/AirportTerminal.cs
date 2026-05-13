using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Airports;

/// <summary>Identificador de terminal en aeropuerto (<c>terminal</c>, hasta 20 caracteres, opcional).</summary>
public sealed class AirportTerminal : IEquatable<AirportTerminal>
{
    private static readonly Regex Pattern = new("^[A-Z0-9 .\\-]{1,20}$", RegexOptions.Compiled);

    public string Value { get; }

    private AirportTerminal(string value) => Value = value;

    public static AirportTerminal? CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("La terminal admite hasta 20 caracteres (letras, números, espacio, punto o guion).", nameof(value));
        return new AirportTerminal(v);
    }

    public bool Equals(AirportTerminal? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is AirportTerminal o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(AirportTerminal? a, AirportTerminal? b) => Equals(a, b);
    public static bool operator !=(AirportTerminal? a, AirportTerminal? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aviation;

/// <summary>Matrícula de aeronave (<c>registration</c>, hasta 20 caracteres).</summary>
public sealed class AircraftRegistration : IEquatable<AircraftRegistration>
{
    private static readonly Regex Pattern = new("^[A-Z0-9\\-]{1,20}$", RegexOptions.Compiled);

    public string Value { get; }

    private AircraftRegistration(string value) => Value = value;

    public static AircraftRegistration Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La matrícula no puede estar vacía.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("La matrícula debe tener entre 1 y 20 caracteres (A-Z, 0-9, guion).", nameof(value));
        return new AircraftRegistration(v);
    }

    public bool Equals(AircraftRegistration? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is AircraftRegistration o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(AircraftRegistration? a, AircraftRegistration? b) => Equals(a, b);
    public static bool operator !=(AircraftRegistration? a, AircraftRegistration? b) => !Equals(a, b);
}

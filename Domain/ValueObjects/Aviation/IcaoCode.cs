using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aviation;

/// <summary>Código ICAO de aeropuerto (4 letras), alineado a <c>VARCHAR(4)</c> en BD.</summary>
public sealed class IcaoCode : IEquatable<IcaoCode>
{
    private static readonly Regex Pattern = new("^[A-Z]{4}$", RegexOptions.Compiled);

    public string Value { get; }

    private IcaoCode(string value) => Value = value;

    public static IcaoCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código ICAO no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código ICAO debe ser exactamente 4 letras (A-Z).", nameof(value));
        return new IcaoCode(v);
    }

    public bool Equals(IcaoCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IcaoCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(IcaoCode? a, IcaoCode? b) => Equals(a, b);
    public static bool operator !=(IcaoCode? a, IcaoCode? b) => !Equals(a, b);
}

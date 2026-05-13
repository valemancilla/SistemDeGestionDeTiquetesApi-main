using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aviation;

/// <summary>Código IATA de aerolínea o aeropuerto (3 letras), alineado a <c>VARCHAR(3)</c> en BD.</summary>
public sealed class IataCode : IEquatable<IataCode>
{
    private static readonly Regex Pattern = new("^[A-Z]{3}$", RegexOptions.Compiled);

    public string Value { get; }

    private IataCode(string value) => Value = value;

    public static IataCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código IATA no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código IATA debe ser exactamente 3 letras (A-Z).", nameof(value));
        return new IataCode(v);
    }

    public bool Equals(IataCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IataCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(IataCode? a, IataCode? b) => Equals(a, b);
    public static bool operator !=(IataCode? a, IataCode? b) => !Equals(a, b);
}

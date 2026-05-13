using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Geography;

/// <summary>Código ISO de país (<c>iso_code</c>, 2 o 3 letras según uso en catálogo).</summary>
public sealed class IsoCountryCode : IEquatable<IsoCountryCode>
{
    private static readonly Regex Pattern = new("^[A-Z]{2,3}$", RegexOptions.Compiled);

    public string Value { get; }

    private IsoCountryCode(string value) => Value = value;

    public static IsoCountryCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código ISO no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código ISO debe tener 2 o 3 letras (A-Z).", nameof(value));
        return new IsoCountryCode(v);
    }

    public bool Equals(IsoCountryCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is IsoCountryCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(IsoCountryCode? a, IsoCountryCode? b) => Equals(a, b);
    public static bool operator !=(IsoCountryCode? a, IsoCountryCode? b) => !Equals(a, b);
}

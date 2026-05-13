using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Prefijo telefónico de país (<c>codigo_pais</c>, hasta 5 caracteres).</summary>
public sealed class PhoneCountryCode : IEquatable<PhoneCountryCode>
{
    private static readonly Regex Pattern = new(@"^\+?[0-9]{1,4}$", RegexOptions.Compiled);

    public string Value { get; }

    private PhoneCountryCode(string value) => Value = value;

    public static PhoneCountryCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de país telefónico no puede estar vacío.", nameof(value));
        var v = value.Trim();
        if (v.Length > 5 || !Pattern.IsMatch(v))
            throw new ArgumentException("El código de país telefónico debe tener hasta 5 caracteres (+ opcional y 1 a 4 dígitos).", nameof(value));
        return new PhoneCountryCode(v);
    }

    public bool Equals(PhoneCountryCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PhoneCountryCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(PhoneCountryCode? a, PhoneCountryCode? b) => Equals(a, b);
    public static bool operator !=(PhoneCountryCode? a, PhoneCountryCode? b) => !Equals(a, b);
}

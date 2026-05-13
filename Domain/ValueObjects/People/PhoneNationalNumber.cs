using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Número telefónico nacional (<c>phone_number</c>, hasta 20 caracteres).</summary>
public sealed class PhoneNationalNumber : IEquatable<PhoneNationalNumber>
{
    private static readonly Regex Pattern = new("^[0-9 \\-]{1,20}$", RegexOptions.Compiled);

    public string Value { get; }

    private PhoneNationalNumber(string value) => Value = value;

    public static PhoneNationalNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de teléfono no puede estar vacío.", nameof(value));
        var v = value.Trim();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El número solo puede contener dígitos, espacios o guiones (máx. 20 caracteres).", nameof(value));
        var digits = Regex.Replace(v, "[^0-9]", "", RegexOptions.None);
        if (digits.Length is < 5 or > 15)
            throw new ArgumentException("El número debe tener entre 5 y 15 dígitos.", nameof(value));
        return new PhoneNationalNumber(v);
    }

    public bool Equals(PhoneNationalNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PhoneNationalNumber o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(PhoneNationalNumber? a, PhoneNationalNumber? b) => Equals(a, b);
    public static bool operator !=(PhoneNationalNumber? a, PhoneNationalNumber? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Aircraft;

/// <summary>Letras de fila de asientos por cabina (<c>letras_asientos</c>, hasta 10 letras).</summary>
public sealed class CabinSeatLetters : IEquatable<CabinSeatLetters>
{
    private static readonly Regex Pattern = new("^[A-Z]{1,10}$", RegexOptions.Compiled);

    public string Value { get; }

    private CabinSeatLetters(string value) => Value = value;

    public static CabinSeatLetters Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Las letras de asiento no pueden estar vacías.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("Las letras deben ser de 1 a 10 caracteres (solo A-Z).", nameof(value));
        return new CabinSeatLetters(v);
    }

    public bool Equals(CabinSeatLetters? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is CabinSeatLetters o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(CabinSeatLetters? a, CabinSeatLetters? b) => Equals(a, b);
    public static bool operator !=(CabinSeatLetters? a, CabinSeatLetters? b) => !Equals(a, b);
}

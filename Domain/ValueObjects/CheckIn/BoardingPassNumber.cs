using System.Text.RegularExpressions;

namespace Domain.ValueObjects.CheckIn;

/// <summary>Número de tarjeta de embarque (<c>numero_tarjeta_embarque</c>, hasta 20 caracteres).</summary>
public sealed class BoardingPassNumber : IEquatable<BoardingPassNumber>
{
    private static readonly Regex Pattern = new("^[A-Z0-9]{1,20}$", RegexOptions.Compiled);

    public string Value { get; }

    private BoardingPassNumber(string value) => Value = value;

    public static BoardingPassNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El número de tarjeta de embarque no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El número debe tener entre 1 y 20 caracteres (A-Z, 0-9).", nameof(value));
        return new BoardingPassNumber(v);
    }

    public bool Equals(BoardingPassNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is BoardingPassNumber o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(BoardingPassNumber? a, BoardingPassNumber? b) => Equals(a, b);
    public static bool operator !=(BoardingPassNumber? a, BoardingPassNumber? b) => !Equals(a, b);
}

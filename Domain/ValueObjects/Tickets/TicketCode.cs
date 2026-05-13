using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Tickets;

/// <summary>Código de tiquete (<c>ticket_code</c>, hasta 30 caracteres).</summary>
public sealed class TicketCode : IEquatable<TicketCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9]{1,30}$", RegexOptions.Compiled);

    public string Value { get; }

    private TicketCode(string value) => Value = value;

    public static TicketCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de tiquete no puede estar vacío.", nameof(value));
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código de tiquete debe tener entre 1 y 30 caracteres (A-Z, 0-9).", nameof(value));
        return new TicketCode(v);
    }

    public bool Equals(TicketCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is TicketCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(TicketCode? a, TicketCode? b) => Equals(a, b);
    public static bool operator !=(TicketCode? a, TicketCode? b) => !Equals(a, b);
}

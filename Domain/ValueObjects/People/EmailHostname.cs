using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Dominio de correo (<c>domain</c>, hasta 100 caracteres, con al menos un punto).</summary>
public sealed class EmailHostname : IEquatable<EmailHostname>
{
    private static readonly Regex Pattern = new(
        @"^(?=.{4,100}$)(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+[a-z]{2,63}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; }

    private EmailHostname(string value) => Value = value;

    public static EmailHostname Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El dominio no puede estar vacío.", nameof(value));
        var v = value.Trim().ToLowerInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El dominio debe ser un nombre DNS válido (ej. ejemplo.com).", nameof(value));
        return new EmailHostname(v);
    }

    public bool Equals(EmailHostname? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is EmailHostname o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(EmailHostname? a, EmailHostname? b) => Equals(a, b);
    public static bool operator !=(EmailHostname? a, EmailHostname? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.People;

/// <summary>Parte local del correo (<c>email_username</c>, hasta 100 caracteres).</summary>
public sealed class EmailLocalPart : IEquatable<EmailLocalPart>
{
    private static readonly Regex Pattern = new("^[a-z0-9](?:[a-z0-9._+-]{0,98}[a-z0-9])?$", RegexOptions.Compiled);

    public string Value { get; }

    private EmailLocalPart(string value) => Value = value;

    public static EmailLocalPart Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La parte local del correo no puede estar vacía.", nameof(value));
        var v = value.Trim().ToLowerInvariant();
        if (v.Length > 100 || !Pattern.IsMatch(v))
            throw new ArgumentException("La parte local del correo no es válida (1-100 caracteres, formato típico RFC).", nameof(value));
        return new EmailLocalPart(v);
    }

    public bool Equals(EmailLocalPart? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is EmailLocalPart o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(EmailLocalPart? a, EmailLocalPart? b) => Equals(a, b);
    public static bool operator !=(EmailLocalPart? a, EmailLocalPart? b) => !Equals(a, b);
}

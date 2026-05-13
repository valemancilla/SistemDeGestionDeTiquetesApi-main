using System.Net;

namespace Domain.ValueObjects.Auth;

/// <summary>Dirección IP de origen de sesión (<c>source_ip</c>, opcional, máx. 45 caracteres).</summary>
public sealed class SessionIpAddress : IEquatable<SessionIpAddress>
{
    public string Value { get; }

    private SessionIpAddress(string value) => Value = value;

    public static SessionIpAddress? CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        var v = value.Trim();
        if (v.Length > 45)
            throw new ArgumentException("La dirección IP no puede superar 45 caracteres.", nameof(value));
        if (!IPAddress.TryParse(v, out _))
            throw new ArgumentException("La dirección IP no es válida.", nameof(value));
        return new SessionIpAddress(v);
    }

    public bool Equals(SessionIpAddress? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SessionIpAddress o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(SessionIpAddress? a, SessionIpAddress? b) => Equals(a, b);
    public static bool operator !=(SessionIpAddress? a, SessionIpAddress? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Auth;

/// <summary>
/// Nombre de usuario (<c>username</c>, 3 a 50 caracteres).
/// Acepta solo mayúsculas, solo minúsculas o combinación (p. ej. <c>Admin</c>, <c>ADMIN</c>, <c>admin</c>); no se convierte a otro casing.
/// </summary>
public sealed class Username : IEquatable<Username>
{
    private static readonly Regex Pattern = new("^[a-zA-Z0-9._-]{3,50}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; }

    private Username(string value) => Value = value;

    public static Username Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(value));
        var v = value.Trim();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException(
                "El usuario debe tener entre 3 y 50 caracteres. Puede usar letras en MAYÚSCULAS, en minúsculas o mezcladas, además de números, punto (.), guion (-) o guion bajo (_).",
                nameof(value));
        return new Username(v);
    }

    public bool Equals(Username? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Username o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(Username? a, Username? b) => Equals(a, b);
    public static bool operator !=(Username? a, Username? b) => !Equals(a, b);
}

using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Addresses;

/// <summary>Código postal (<c>codigo_postal</c>, hasta 20 caracteres, opcional).</summary>
public sealed class PostalCode : IEquatable<PostalCode>
{
    private static readonly Regex Pattern = new("^[A-Z0-9 \\-]{1,20}$", RegexOptions.Compiled);

    public string Value { get; }

    private PostalCode(string value) => Value = value;

    /// <summary>Devuelve <c>null</c> si el valor es nulo o solo espacios.</summary>
    public static PostalCode? CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        var v = value.Trim().ToUpperInvariant();
        if (!Pattern.IsMatch(v))
            throw new ArgumentException("El código postal admite hasta 20 caracteres (letras, números, espacio o guion).", nameof(value));
        return new PostalCode(v);
    }

    public bool Equals(PostalCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PostalCode o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(PostalCode? a, PostalCode? b) => Equals(a, b);
    public static bool operator !=(PostalCode? a, PostalCode? b) => !Equals(a, b);
}

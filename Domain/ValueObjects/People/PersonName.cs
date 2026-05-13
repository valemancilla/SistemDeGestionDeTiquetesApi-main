namespace Domain.ValueObjects.People;

/// <summary>Nombre o apellido de persona (<c>first_name</c> / <c>last_name</c>, hasta 100 caracteres).</summary>
public sealed class PersonName : IEquatable<PersonName>
{
    public string Value { get; }

    private PersonName(string value) => Value = value;

    public static PersonName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(value));
        var v = value.Trim();
        if (v.Length > 100)
            throw new ArgumentException("El nombre no puede superar 100 caracteres.", nameof(value));
        for (var i = 0; i < v.Length; i++)
        {
            if (char.IsControl(v[i]))
                throw new ArgumentException("El nombre no puede contener caracteres de control.", nameof(value));
        }

        return new PersonName(v);
    }

    public bool Equals(PersonName? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PersonName o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
    public static bool operator ==(PersonName? a, PersonName? b) => Equals(a, b);
    public static bool operator !=(PersonName? a, PersonName? b) => !Equals(a, b);
}

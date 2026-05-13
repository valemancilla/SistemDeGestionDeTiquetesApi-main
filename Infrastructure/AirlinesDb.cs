namespace Infrastructure;

/// <summary>Esquema <c>airlinesdb</c> del DDL canónico; historial EF en <c>__efmigrationshistory</c>. <see cref="LegacySchema"/>: tablas no incluidas en el DDL.</summary>
public static class AirlinesDb
{
    public const string Schema = "airlinesdb";

    /// <summary>Tablas legacy fuera del DDL compartido.</summary>
    public const string LegacySchema = "public";
}

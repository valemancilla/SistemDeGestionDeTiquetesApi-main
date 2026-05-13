using System.IO;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure;

/// <summary>
/// Asegura que <c>dotnet ef</c> use la misma cadena que <c>Api/appsettings*.json</c>
/// (independiente de <c>--configuration Release</c> u otro entorno del host).
/// </summary>
public sealed class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var apiRoot = FindApiDirectory();
        var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiRoot)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{aspEnv}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:Postgres no está definido. " +
                "Configúralo en Api/appsettings.json o Api/appsettings.Development.json.");

        var csb = new NpgsqlConnectionStringBuilder(connectionString);
        Console.WriteLine(
            $"[EF design-time] Base de datos: {csb.Database} | Host: {csb.Host}. " +
            "Si usas variables de entorno (p. ej. ConnectionStrings__Postgres), prevalecen sobre appsettings; " +
            "el script BaselinePublicMigrationsHistory.sql debe ejecutarse en esta misma base.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString, npgsql =>
        {
            npgsql.MigrationsHistoryTable("__efmigrationshistory", AirlinesDb.LegacySchema);
        });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return new AppDbContext(optionsBuilder.Options);
    }

    private static string FindApiDirectory()
    {
        var start = new DirectoryInfo(Directory.GetCurrentDirectory());
        if (File.Exists(Path.Combine(start.FullName, "appsettings.json"))
            && start.Name.Equals("Api", StringComparison.OrdinalIgnoreCase))
            return start.FullName;

        for (var dir = start; dir != null; dir = dir.Parent)
        {
            var api = Path.Combine(dir.FullName, "Api", "appsettings.json");
            if (File.Exists(api))
                return Path.GetDirectoryName(api)!;
        }

        var infraDir = Path.GetDirectoryName(typeof(AppDbContext).Assembly.Location);
        if (!string.IsNullOrEmpty(infraDir))
        {
            for (var dir = new DirectoryInfo(infraDir); dir != null; dir = dir.Parent)
            {
                var api = Path.Combine(dir.FullName, "Api", "appsettings.json");
                if (File.Exists(api))
                    return Path.GetDirectoryName(api)!;
            }
        }

        throw new InvalidOperationException(
            "No se encontró Api/appsettings.json. Ejecuta dotnet ef desde la carpeta del repositorio o desde Api.");
    }
}

using Domain.Entities.Auth;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>systemroles</c>. Entidad: <see cref="SystemRole"/>.</summary>
public static class SystemRolesCatalog
{
    public const string TableName = "system_roles";
    public const int NombreMaxLength = 50;
    public const int DescripcionMaxLength = 150;

    public const int AdministradorId = 1;
    public const string AdministradorNombre = "Administrador";
    public const string AdministradorDescripcion = "Acceso total al sistema.";

    public const int OperadorId = 2;
    public const string OperadorNombre = "Operador";
    public const string OperadorDescripcion = "Gestión operativa de vuelos y reservas.";

    public const int ClienteId = 3;
    public const string ClienteNombre = "Cliente";
    public const string ClienteDescripcion = "Usuario final (portal de reservas).";
}

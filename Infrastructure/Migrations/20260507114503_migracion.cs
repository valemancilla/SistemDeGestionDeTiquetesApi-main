using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Base ya puede tener esta tabla (p. ej. creada antes sin fila en __EFMigrationsHistory).
            migrationBuilder.Sql("""
                CREATE TABLE IF NOT EXISTS products (
                    "Id" uuid NOT NULL,
                    "Name" character varying(50) NOT NULL,
                    sku character varying(64) NOT NULL,
                    price numeric(18,2) NOT NULL,
                    "Stock" integer NOT NULL,
                    "CreatedAt" timestamp with time zone NOT NULL,
                    CONSTRAINT "PK_products" PRIMARY KEY ("Id")
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP TABLE IF EXISTS products;""");
        }
    }
}

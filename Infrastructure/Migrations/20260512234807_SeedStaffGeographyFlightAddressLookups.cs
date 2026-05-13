using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaffGeographyFlightAddressLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "availabilitystatuses",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "No disponible" },
                    { 3, "En licencia" }
                });

            migrationBuilder.InsertData(
                table: "cabintypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Económica" },
                    { 2, "Premium economy" },
                    { 3, "Business" },
                    { 4, "Primera" }
                });

            migrationBuilder.InsertData(
                table: "continents",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "América" },
                    { 2, "Europa" },
                    { 3, "Asia" },
                    { 4, "África" },
                    { 5, "Oceanía" },
                    { 6, "Antártida" }
                });

            migrationBuilder.InsertData(
                table: "flightroles",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Piloto comandante" },
                    { 2, "Piloto copiloto" },
                    { 3, "Jefe de cabina" },
                    { 4, "Tripulante de cabina" }
                });

            migrationBuilder.InsertData(
                table: "roadtypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Calle" },
                    { 2, "Carrera" },
                    { 3, "Avenida" },
                    { 4, "Diagonal" },
                    { 5, "Transversal" },
                    { 6, "Circunvalar" }
                });

            migrationBuilder.InsertData(
                table: "seatlocationtypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Ventana" },
                    { 2, "Centro" },
                    { 3, "Pasillo" }
                });

            migrationBuilder.InsertData(
                table: "staffroles",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Comandante" },
                    { 2, "Copiloto" },
                    { 3, "Ingeniero de vuelo" },
                    { 4, "Sobrecargo" },
                    { 5, "Auxiliar de cabina" }
                });

            migrationBuilder.Sql("""
                SELECT setval(pg_get_serial_sequence('availabilitystatuses', 'Id'), COALESCE((SELECT MAX("Id") FROM availabilitystatuses), 1));
                SELECT setval(pg_get_serial_sequence('cabintypes', 'Id'), COALESCE((SELECT MAX("Id") FROM cabintypes), 1));
                SELECT setval(pg_get_serial_sequence('continents', 'Id'), COALESCE((SELECT MAX("Id") FROM continents), 1));
                SELECT setval(pg_get_serial_sequence('flightroles', 'Id'), COALESCE((SELECT MAX("Id") FROM flightroles), 1));
                SELECT setval(pg_get_serial_sequence('roadtypes', 'Id'), COALESCE((SELECT MAX("Id") FROM roadtypes), 1));
                SELECT setval(pg_get_serial_sequence('seatlocationtypes', 'Id'), COALESCE((SELECT MAX("Id") FROM seatlocationtypes), 1));
                SELECT setval(pg_get_serial_sequence('staffroles', 'Id'), COALESCE((SELECT MAX("Id") FROM staffroles), 1));
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "availabilitystatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "availabilitystatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "availabilitystatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cabintypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cabintypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cabintypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cabintypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "continents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "flightroles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "flightroles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "flightroles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "flightroles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "roadtypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "seatlocationtypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seatlocationtypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seatlocationtypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "staffroles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "staffroles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "staffroles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "staffroles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "staffroles",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

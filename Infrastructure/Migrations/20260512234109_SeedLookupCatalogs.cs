using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookupCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cardissuers",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Visa" },
                    { 2, "Mastercard" },
                    { 3, "American Express" }
                });

            migrationBuilder.InsertData(
                table: "cardtypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Crédito" },
                    { 2, "Débito" },
                    { 3, "Prepago" }
                });

            migrationBuilder.InsertData(
                table: "checkinstatuses",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Completado" },
                    { 3, "No show" }
                });

            migrationBuilder.InsertData(
                table: "flightstates",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Programado" },
                    { 2, "En vuelo" },
                    { 3, "Aterrizado" },
                    { 4, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "invoiceitemtypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Tarifa base" },
                    { 2, "Impuestos" },
                    { 3, "Tasas aeroportuarias" },
                    { 4, "Cargos por servicio" }
                });

            migrationBuilder.InsertData(
                table: "paymentmethodtypes",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Tarjeta de crédito" },
                    { 2, "Tarjeta de débito" },
                    { 3, "Efectivo" },
                    { 4, "Transferencia" }
                });

            migrationBuilder.InsertData(
                table: "paymentstates",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Pagado" },
                    { 3, "Rechazado" },
                    { 4, "Reembolsado" }
                });

            migrationBuilder.InsertData(
                table: "reservationstatuses",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Creada" },
                    { 2, "Confirmada" },
                    { 3, "Cancelada" },
                    { 4, "Completada" }
                });

            migrationBuilder.InsertData(
                table: "systemroles",
                columns: new[] { "Id", "descripcion", "nombre" },
                values: new object[,]
                {
                    { 1, "Acceso total al sistema.", "Administrador" },
                    { 2, "Gestión operativa de vuelos y reservas.", "Operador" },
                    { 3, "Usuario final (portal de reservas).", "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "ticketstatuses",
                columns: new[] { "Id", "nombre" },
                values: new object[,]
                {
                    { 1, "Emitido" },
                    { 2, "Usado" },
                    { 3, "Cancelado" },
                    { 4, "Reembolsado" }
                });

            migrationBuilder.InsertData(
                table: "flightstatustransitions",
                columns: new[] { "Id", "estado_destino_id", "estado_origen_id" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 2 },
                    { 3, 4, 1 },
                    { 4, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "reservationstatustransitions",
                columns: new[] { "Id", "estado_destino_id", "estado_origen_id" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 3, 3, 1 },
                    { 4, 3, 2 }
                });

            // Alinear secuencias de identity tras InsertData (PostgreSQL).
            migrationBuilder.Sql("""
                SELECT setval(pg_get_serial_sequence('cardissuers', 'Id'), COALESCE((SELECT MAX("Id") FROM cardissuers), 1));
                SELECT setval(pg_get_serial_sequence('cardtypes', 'Id'), COALESCE((SELECT MAX("Id") FROM cardtypes), 1));
                SELECT setval(pg_get_serial_sequence('checkinstatuses', 'Id'), COALESCE((SELECT MAX("Id") FROM checkinstatuses), 1));
                SELECT setval(pg_get_serial_sequence('flightstates', 'Id'), COALESCE((SELECT MAX("Id") FROM flightstates), 1));
                SELECT setval(pg_get_serial_sequence('invoiceitemtypes', 'Id'), COALESCE((SELECT MAX("Id") FROM invoiceitemtypes), 1));
                SELECT setval(pg_get_serial_sequence('paymentmethodtypes', 'Id'), COALESCE((SELECT MAX("Id") FROM paymentmethodtypes), 1));
                SELECT setval(pg_get_serial_sequence('paymentstates', 'Id'), COALESCE((SELECT MAX("Id") FROM paymentstates), 1));
                SELECT setval(pg_get_serial_sequence('reservationstatuses', 'Id'), COALESCE((SELECT MAX("Id") FROM reservationstatuses), 1));
                SELECT setval(pg_get_serial_sequence('systemroles', 'Id'), COALESCE((SELECT MAX("Id") FROM systemroles), 1));
                SELECT setval(pg_get_serial_sequence('ticketstatuses', 'Id'), COALESCE((SELECT MAX("Id") FROM ticketstatuses), 1));
                SELECT setval(pg_get_serial_sequence('flightstatustransitions', 'Id'), COALESCE((SELECT MAX("Id") FROM flightstatustransitions), 1));
                SELECT setval(pg_get_serial_sequence('reservationstatustransitions', 'Id'), COALESCE((SELECT MAX("Id") FROM reservationstatustransitions), 1));
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cardissuers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cardissuers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cardissuers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cardtypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cardtypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cardtypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "checkinstatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "checkinstatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "checkinstatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "flightstatustransitions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "flightstatustransitions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "flightstatustransitions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "flightstatustransitions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "invoiceitemtypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "invoiceitemtypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "invoiceitemtypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "invoiceitemtypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "paymentmethodtypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "paymentmethodtypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "paymentmethodtypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "paymentmethodtypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "paymentstates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "paymentstates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "paymentstates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "paymentstates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "reservationstatustransitions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "reservationstatustransitions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "reservationstatustransitions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "reservationstatustransitions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "systemroles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "systemroles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "systemroles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ticketstatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ticketstatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ticketstatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ticketstatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "flightstates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "flightstates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "flightstates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "flightstates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "reservationstatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "reservationstatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "reservationstatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "reservationstatuses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignDocumentTypeAirlinesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "users",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "tickets",
                newName: "tickets",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "ticket_statuses",
                newName: "ticket_statuses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "system_roles",
                newName: "system_roles",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "staffavailability",
                newName: "staffavailability",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "staff_positions",
                newName: "staff_positions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "staff",
                newName: "staff",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "sessions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "seat_location_types",
                newName: "seat_location_types",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "seasons",
                newName: "seasons",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "routestops",
                newName: "routestops",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "routes",
                newName: "routes",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "rolepermissions",
                newName: "rolepermissions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "roadtypes",
                newName: "roadtypes",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "reservationstatus",
                newName: "reservationstatus",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "reservations",
                newName: "reservations",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "reservationpassengers",
                newName: "reservationpassengers",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "reservationflights",
                newName: "reservationflights",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "reservation_status_transitions",
                newName: "reservation_status_transitions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "regions",
                newName: "regions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "phonecodes",
                newName: "phonecodes",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "personphones",
                newName: "personphones",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "personemails",
                newName: "personemails",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "permissions",
                newName: "permissions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "people",
                newName: "people",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "payments",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "paymentmethods",
                newName: "paymentmethods",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "payment_statuses",
                newName: "payment_statuses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "payment_method_types",
                newName: "payment_method_types",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "passengers",
                newName: "passengers",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "passenger_types",
                newName: "passenger_types",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "invoices",
                newName: "invoices",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "invoiceitems",
                newName: "invoiceitems",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "invoice_item_types",
                newName: "invoice_item_types",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flightstatustransitions",
                newName: "flightstatustransitions",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flightseats",
                newName: "flightseats",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flights",
                newName: "flights",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flightassignments",
                newName: "flightassignments",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flight_statuses",
                newName: "flight_statuses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "flight_roles",
                newName: "flight_roles",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "fares",
                newName: "fares",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "emaildomains",
                newName: "emaildomains",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "documenttypes",
                newName: "documenttypes",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "countries",
                newName: "countries",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "continents",
                newName: "continents",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "clients",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "cities",
                newName: "cities",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "checkins",
                newName: "checkins",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "checkin_statuses",
                newName: "checkin_statuses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "card_types",
                newName: "card_types",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "card_issuers",
                newName: "card_issuers",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "cabintypes",
                newName: "cabintypes",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "cabinconfiguration",
                newName: "cabinconfiguration",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "availability_statuses",
                newName: "availability_statuses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "airports",
                newName: "airports",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "airportairline",
                newName: "airportairline",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "airlines",
                newName: "airlines",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "aircraftmodels",
                newName: "aircraftmodels",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "aircraftmanufacturers",
                newName: "aircraftmanufacturers",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "aircraft",
                newName: "aircraft",
                newSchema: "airlinesdb");

            migrationBuilder.RenameTable(
                name: "addresses",
                newName: "addresses",
                newSchema: "airlinesdb");

            migrationBuilder.RenameColumn(
                name: "nombre",
                schema: "airlinesdb",
                table: "documenttypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "codigo",
                schema: "airlinesdb",
                table: "documenttypes",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_documenttypes_nombre",
                schema: "airlinesdb",
                table: "documenttypes",
                newName: "IX_DocumentTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_documenttypes_codigo",
                schema: "airlinesdb",
                table: "documenttypes",
                newName: "IX_DocumentTypes_Code");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "airlinesdb",
                table: "documenttypes",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "airlinesdb",
                table: "documenttypes",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users",
                schema: "airlinesdb",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "tickets",
                schema: "airlinesdb",
                newName: "tickets");

            migrationBuilder.RenameTable(
                name: "ticket_statuses",
                schema: "airlinesdb",
                newName: "ticket_statuses");

            migrationBuilder.RenameTable(
                name: "system_roles",
                schema: "airlinesdb",
                newName: "system_roles");

            migrationBuilder.RenameTable(
                name: "staffavailability",
                schema: "airlinesdb",
                newName: "staffavailability");

            migrationBuilder.RenameTable(
                name: "staff_positions",
                schema: "airlinesdb",
                newName: "staff_positions");

            migrationBuilder.RenameTable(
                name: "staff",
                schema: "airlinesdb",
                newName: "staff");

            migrationBuilder.RenameTable(
                name: "sessions",
                schema: "airlinesdb",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "seat_location_types",
                schema: "airlinesdb",
                newName: "seat_location_types");

            migrationBuilder.RenameTable(
                name: "seasons",
                schema: "airlinesdb",
                newName: "seasons");

            migrationBuilder.RenameTable(
                name: "routestops",
                schema: "airlinesdb",
                newName: "routestops");

            migrationBuilder.RenameTable(
                name: "routes",
                schema: "airlinesdb",
                newName: "routes");

            migrationBuilder.RenameTable(
                name: "rolepermissions",
                schema: "airlinesdb",
                newName: "rolepermissions");

            migrationBuilder.RenameTable(
                name: "roadtypes",
                schema: "airlinesdb",
                newName: "roadtypes");

            migrationBuilder.RenameTable(
                name: "reservationstatus",
                schema: "airlinesdb",
                newName: "reservationstatus");

            migrationBuilder.RenameTable(
                name: "reservations",
                schema: "airlinesdb",
                newName: "reservations");

            migrationBuilder.RenameTable(
                name: "reservationpassengers",
                schema: "airlinesdb",
                newName: "reservationpassengers");

            migrationBuilder.RenameTable(
                name: "reservationflights",
                schema: "airlinesdb",
                newName: "reservationflights");

            migrationBuilder.RenameTable(
                name: "reservation_status_transitions",
                schema: "airlinesdb",
                newName: "reservation_status_transitions");

            migrationBuilder.RenameTable(
                name: "regions",
                schema: "airlinesdb",
                newName: "regions");

            migrationBuilder.RenameTable(
                name: "phonecodes",
                schema: "airlinesdb",
                newName: "phonecodes");

            migrationBuilder.RenameTable(
                name: "personphones",
                schema: "airlinesdb",
                newName: "personphones");

            migrationBuilder.RenameTable(
                name: "personemails",
                schema: "airlinesdb",
                newName: "personemails");

            migrationBuilder.RenameTable(
                name: "permissions",
                schema: "airlinesdb",
                newName: "permissions");

            migrationBuilder.RenameTable(
                name: "people",
                schema: "airlinesdb",
                newName: "people");

            migrationBuilder.RenameTable(
                name: "payments",
                schema: "airlinesdb",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "paymentmethods",
                schema: "airlinesdb",
                newName: "paymentmethods");

            migrationBuilder.RenameTable(
                name: "payment_statuses",
                schema: "airlinesdb",
                newName: "payment_statuses");

            migrationBuilder.RenameTable(
                name: "payment_method_types",
                schema: "airlinesdb",
                newName: "payment_method_types");

            migrationBuilder.RenameTable(
                name: "passengers",
                schema: "airlinesdb",
                newName: "passengers");

            migrationBuilder.RenameTable(
                name: "passenger_types",
                schema: "airlinesdb",
                newName: "passenger_types");

            migrationBuilder.RenameTable(
                name: "invoices",
                schema: "airlinesdb",
                newName: "invoices");

            migrationBuilder.RenameTable(
                name: "invoiceitems",
                schema: "airlinesdb",
                newName: "invoiceitems");

            migrationBuilder.RenameTable(
                name: "invoice_item_types",
                schema: "airlinesdb",
                newName: "invoice_item_types");

            migrationBuilder.RenameTable(
                name: "flightstatustransitions",
                schema: "airlinesdb",
                newName: "flightstatustransitions");

            migrationBuilder.RenameTable(
                name: "flightseats",
                schema: "airlinesdb",
                newName: "flightseats");

            migrationBuilder.RenameTable(
                name: "flights",
                schema: "airlinesdb",
                newName: "flights");

            migrationBuilder.RenameTable(
                name: "flightassignments",
                schema: "airlinesdb",
                newName: "flightassignments");

            migrationBuilder.RenameTable(
                name: "flight_statuses",
                schema: "airlinesdb",
                newName: "flight_statuses");

            migrationBuilder.RenameTable(
                name: "flight_roles",
                schema: "airlinesdb",
                newName: "flight_roles");

            migrationBuilder.RenameTable(
                name: "fares",
                schema: "airlinesdb",
                newName: "fares");

            migrationBuilder.RenameTable(
                name: "emaildomains",
                schema: "airlinesdb",
                newName: "emaildomains");

            migrationBuilder.RenameTable(
                name: "documenttypes",
                schema: "airlinesdb",
                newName: "documenttypes");

            migrationBuilder.RenameTable(
                name: "countries",
                schema: "airlinesdb",
                newName: "countries");

            migrationBuilder.RenameTable(
                name: "continents",
                schema: "airlinesdb",
                newName: "continents");

            migrationBuilder.RenameTable(
                name: "clients",
                schema: "airlinesdb",
                newName: "clients");

            migrationBuilder.RenameTable(
                name: "cities",
                schema: "airlinesdb",
                newName: "cities");

            migrationBuilder.RenameTable(
                name: "checkins",
                schema: "airlinesdb",
                newName: "checkins");

            migrationBuilder.RenameTable(
                name: "checkin_statuses",
                schema: "airlinesdb",
                newName: "checkin_statuses");

            migrationBuilder.RenameTable(
                name: "card_types",
                schema: "airlinesdb",
                newName: "card_types");

            migrationBuilder.RenameTable(
                name: "card_issuers",
                schema: "airlinesdb",
                newName: "card_issuers");

            migrationBuilder.RenameTable(
                name: "cabintypes",
                schema: "airlinesdb",
                newName: "cabintypes");

            migrationBuilder.RenameTable(
                name: "cabinconfiguration",
                schema: "airlinesdb",
                newName: "cabinconfiguration");

            migrationBuilder.RenameTable(
                name: "availability_statuses",
                schema: "airlinesdb",
                newName: "availability_statuses");

            migrationBuilder.RenameTable(
                name: "airports",
                schema: "airlinesdb",
                newName: "airports");

            migrationBuilder.RenameTable(
                name: "airportairline",
                schema: "airlinesdb",
                newName: "airportairline");

            migrationBuilder.RenameTable(
                name: "airlines",
                schema: "airlinesdb",
                newName: "airlines");

            migrationBuilder.RenameTable(
                name: "aircraftmodels",
                schema: "airlinesdb",
                newName: "aircraftmodels");

            migrationBuilder.RenameTable(
                name: "aircraftmanufacturers",
                schema: "airlinesdb",
                newName: "aircraftmanufacturers");

            migrationBuilder.RenameTable(
                name: "aircraft",
                schema: "airlinesdb",
                newName: "aircraft");

            migrationBuilder.RenameTable(
                name: "addresses",
                schema: "airlinesdb",
                newName: "addresses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "documenttypes",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "documenttypes",
                newName: "codigo");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Name",
                table: "documenttypes",
                newName: "IX_documenttypes_nombre");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Code",
                table: "documenttypes",
                newName: "IX_documenttypes_codigo");

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "documenttypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "documenttypes",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}

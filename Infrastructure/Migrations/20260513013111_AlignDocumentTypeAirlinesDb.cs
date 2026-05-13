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

            // Idempotente: si el DDL canónico ya creó tablas en airlinesdb y EF dejó copias en public,
            // ALTER ... SET SCHEMA fallaría (42P07). Solo movemos cuando no hay homónimo en airlinesdb.
            migrationBuilder.Sql(
                """
                DO $$
                DECLARE
                  t text;
                  tables text[] := ARRAY[
                    'users','tickets','ticket_statuses','system_roles','staffavailability','staff_positions','staff','sessions',
                    'seat_location_types','seasons','routestops','routes','rolepermissions','roadtypes','reservationstatus',
                    'reservations','reservationpassengers','reservationflights','reservation_status_transitions','regions',
                    'phonecodes','personphones','personemails','permissions','people','payments','paymentmethods',
                    'payment_statuses','payment_method_types','passengers','passenger_types','invoices','invoiceitems',
                    'invoice_item_types','flightstatustransitions','flightseats','flights','flightassignments',
                    'flight_statuses','flight_roles','fares','emaildomains','documenttypes','countries','continents',
                    'clients','cities','checkins','checkin_statuses','card_types','card_issuers','cabintypes',
                    'cabinconfiguration','availability_statuses','airports','airportairline','airlines','aircraftmodels',
                    'aircraftmanufacturers','aircraft','addresses'
                  ];
                BEGIN
                  FOREACH t IN ARRAY tables
                  LOOP
                    IF EXISTS (
                      SELECT 1 FROM information_schema.tables
                      WHERE table_schema = 'public' AND table_name = t
                    ) AND NOT EXISTS (
                      SELECT 1 FROM information_schema.tables
                      WHERE table_schema = 'airlinesdb' AND table_name = t
                    ) THEN
                      EXECUTE format('ALTER TABLE public.%I SET SCHEMA airlinesdb', t);
                    END IF;
                  END LOOP;
                END $$;
                """);

            migrationBuilder.Sql(
                """
                DO $$
                DECLARE
                  old_nombre_idx text;
                  old_codigo_idx text;
                BEGIN
                  IF NOT EXISTS (
                    SELECT 1 FROM information_schema.tables
                    WHERE table_schema = 'airlinesdb' AND table_name = 'documenttypes'
                  ) THEN
                    RETURN;
                  END IF;

                  IF EXISTS (
                    SELECT 1 FROM information_schema.columns
                    WHERE table_schema = 'airlinesdb' AND table_name = 'documenttypes' AND column_name = 'nombre'
                  ) THEN
                    ALTER TABLE airlinesdb.documenttypes RENAME COLUMN nombre TO "Name";
                    ALTER TABLE airlinesdb.documenttypes RENAME COLUMN codigo TO "Code";
                  END IF;

                  SELECT c.relname INTO old_nombre_idx
                  FROM pg_class c
                  JOIN pg_namespace n ON n.oid = c.relnamespace
                  JOIN pg_index i ON i.indexrelid = c.oid
                  JOIN pg_class t ON t.oid = i.indrelid
                  JOIN pg_namespace tn ON tn.oid = t.relnamespace
                  WHERE n.nspname = 'airlinesdb' AND c.relkind = 'i'
                    AND tn.nspname = 'airlinesdb' AND t.relname = 'documenttypes'
                    AND c.relname IN ('IX_documenttypes_nombre', 'ix_documenttypes_nombre')
                  LIMIT 1;

                  IF old_nombre_idx IS NOT NULL THEN
                    EXECUTE format('ALTER INDEX airlinesdb.%I RENAME TO %I', old_nombre_idx, 'IX_DocumentTypes_Name');
                  END IF;

                  SELECT c.relname INTO old_codigo_idx
                  FROM pg_class c
                  JOIN pg_namespace n ON n.oid = c.relnamespace
                  JOIN pg_index i ON i.indexrelid = c.oid
                  JOIN pg_class t ON t.oid = i.indrelid
                  JOIN pg_namespace tn ON tn.oid = t.relnamespace
                  WHERE n.nspname = 'airlinesdb' AND c.relkind = 'i'
                    AND tn.nspname = 'airlinesdb' AND t.relname = 'documenttypes'
                    AND c.relname IN ('IX_documenttypes_codigo', 'ix_documenttypes_codigo')
                  LIMIT 1;

                  IF old_codigo_idx IS NOT NULL THEN
                    EXECUTE format('ALTER INDEX airlinesdb.%I RENAME TO %I', old_codigo_idx, 'IX_DocumentTypes_Code');
                  END IF;

                  IF EXISTS (
                    SELECT 1 FROM information_schema.columns
                    WHERE table_schema = 'airlinesdb' AND table_name = 'documenttypes' AND column_name = 'Name'
                  ) THEN
                    ALTER TABLE airlinesdb.documenttypes
                      ALTER COLUMN "Name" TYPE character varying(255),
                      ALTER COLUMN "Code" TYPE character varying(255);
                  END IF;
                END $$;
                """);
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

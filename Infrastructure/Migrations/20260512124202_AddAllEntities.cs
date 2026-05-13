using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aircraftmanufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraftmanufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "availabilitystatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availabilitystatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cabintypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabintypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cardissuers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cardissuers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cardtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cardtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "checkinstatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkinstatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "continents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "documenttypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documenttypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emaildomains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dominio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emaildomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flightroles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flightstates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightstates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "invoiceitemtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoiceitemtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "maintenancetypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenancetypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "passengertypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    edad_min = table.Column<int>(type: "integer", nullable: true),
                    edad_max = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengertypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethodtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethodtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paymentstates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentstates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phonecodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_pais = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    nombre_pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phonecodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reservationstatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationstatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roadtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    precio_factor = table.Column<decimal>(type: "numeric(5,4)", nullable: false, defaultValue: 1.0000m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "seatlocationtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seatlocationtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staffroles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "systemroles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_systemroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ticketstatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketstatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aircraftmodels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fabricante_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_modelo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    capacidad_maxima = table.Column<int>(type: "integer", nullable: false),
                    peso_max_despegue_kg = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    consumo_combustible_kg_h = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    velocidad_crucero_kmh = table.Column<int>(type: "integer", nullable: true),
                    altitud_crucero_ft = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraftmodels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aircraftmodels_aircraftmanufacturers_fabricante_id",
                        column: x => x.fabricante_id,
                        principalTable: "aircraftmanufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_iso = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    continente_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_countries_continents_continente_id",
                        column: x => x.continente_id,
                        principalTable: "continents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flightstatustransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado_origen_id = table.Column<int>(type: "integer", nullable: false),
                    estado_destino_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightstatustransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flightstatustransitions_flightstates_estado_destino_id",
                        column: x => x.estado_destino_id,
                        principalTable: "flightstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flightstatustransitions_flightstates_estado_origen_id",
                        column: x => x.estado_origen_id,
                        principalTable: "flightstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "paymentmethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_medio_pago_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_tarjeta_id = table.Column<int>(type: "integer", nullable: true),
                    emisor_tarjeta_id = table.Column<int>(type: "integer", nullable: true),
                    nombre_comercial = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentmethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_paymentmethods_cardissuers_emisor_tarjeta_id",
                        column: x => x.emisor_tarjeta_id,
                        principalTable: "cardissuers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_paymentmethods_cardtypes_tipo_tarjeta_id",
                        column: x => x.tipo_tarjeta_id,
                        principalTable: "cardtypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_paymentmethods_paymentmethodtypes_tipo_medio_pago_id",
                        column: x => x.tipo_medio_pago_id,
                        principalTable: "paymentmethodtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservationstatustransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado_origen_id = table.Column<int>(type: "integer", nullable: false),
                    estado_destino_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationstatustransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservationstatustransitions_reservationstatuses_estado_des~",
                        column: x => x.estado_destino_id,
                        principalTable: "reservationstatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservationstatustransitions_reservationstatuses_estado_ori~",
                        column: x => x.estado_origen_id,
                        principalTable: "reservationstatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rolepermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    permiso_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolepermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rolepermissions_permissions_permiso_id",
                        column: x => x.permiso_id,
                        principalTable: "permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolepermissions_systemroles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "systemroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "airlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    codigo_iata = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    pais_origen_id = table.Column<int>(type: "integer", nullable: false),
                    activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_airlines_countries_pais_origen_id",
                        column: x => x.pais_origen_id,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tipo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pais_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_regions_countries_pais_id",
                        column: x => x.pais_id,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aircraft",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    modelo_id = table.Column<int>(type: "integer", nullable: false),
                    aerolinea_id = table.Column<int>(type: "integer", nullable: false),
                    matricula = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_fabricacion = table.Column<DateOnly>(type: "date", nullable: true),
                    activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aircraft_aircraftmodels_modelo_id",
                        column: x => x.modelo_id,
                        principalTable: "aircraftmodels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aircraft_airlines_aerolinea_id",
                        column: x => x.aerolinea_id,
                        principalTable: "airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    region_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cities_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aircraftmaintenance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aeronave_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_mantenimiento_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraftmaintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aircraftmaintenance_aircraft_aeronave_id",
                        column: x => x.aeronave_id,
                        principalTable: "aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aircraftmaintenance_maintenancetypes_tipo_mantenimiento_id",
                        column: x => x.tipo_mantenimiento_id,
                        principalTable: "maintenancetypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cabinconfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aeronave_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_cabina_id = table.Column<int>(type: "integer", nullable: false),
                    fila_inicio = table.Column<int>(type: "integer", nullable: false),
                    fila_fin = table.Column<int>(type: "integer", nullable: false),
                    asientos_por_fila = table.Column<int>(type: "integer", nullable: false),
                    letras_asientos = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabinconfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cabinconfiguration_aircraft_aeronave_id",
                        column: x => x.aeronave_id,
                        principalTable: "aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cabinconfiguration_cabintypes_tipo_cabina_id",
                        column: x => x.tipo_cabina_id,
                        principalTable: "cabintypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_via_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_via = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ciudad_id = table.Column<int>(type: "integer", nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_addresses_cities_ciudad_id",
                        column: x => x.ciudad_id,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addresses_roadtypes_tipo_via_id",
                        column: x => x.tipo_via_id,
                        principalTable: "roadtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    codigo_iata = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    codigo_icao = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    ciudad_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_airports_cities_ciudad_id",
                        column: x => x.ciudad_id,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_documento_id = table.Column<int>(type: "integer", nullable: false),
                    numero_documento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    genero = table.Column<char>(type: "char(1)", nullable: true),
                    direccion_id = table.Column<int>(type: "integer", nullable: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.Id);
                    table.ForeignKey(
                        name: "FK_people_addresses_direccion_id",
                        column: x => x.direccion_id,
                        principalTable: "addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_people_documenttypes_tipo_documento_id",
                        column: x => x.tipo_documento_id,
                        principalTable: "documenttypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "airportairline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aeropuerto_id = table.Column<int>(type: "integer", nullable: false),
                    aerolinea_id = table.Column<int>(type: "integer", nullable: false),
                    terminal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airportairline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_airportairline_airlines_aerolinea_id",
                        column: x => x.aerolinea_id,
                        principalTable: "airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_airportairline_airports_aeropuerto_id",
                        column: x => x.aeropuerto_id,
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aeropuerto_origen_id = table.Column<int>(type: "integer", nullable: false),
                    aeropuerto_destino_id = table.Column<int>(type: "integer", nullable: false),
                    distancia_km = table.Column<int>(type: "integer", nullable: true),
                    duracion_estimada_min = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_routes_airports_aeropuerto_destino_id",
                        column: x => x.aeropuerto_destino_id,
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_routes_airports_aeropuerto_origen_id",
                        column: x => x.aeropuerto_origen_id,
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_pasajero_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_passengers_passengertypes_tipo_pasajero_id",
                        column: x => x.tipo_pasajero_id,
                        principalTable: "passengertypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_passengers_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    usuario_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dominio_email_id = table.Column<int>(type: "integer", nullable: false),
                    es_principal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personemails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personemails_emaildomains_dominio_email_id",
                        column: x => x.dominio_email_id,
                        principalTable: "emaildomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personemails_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    codigo_telefono_id = table.Column<int>(type: "integer", nullable: false),
                    numero_telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    es_principal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personphones_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personphones_phonecodes_codigo_telefono_id",
                        column: x => x.codigo_telefono_id,
                        principalTable: "phonecodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    cargo_id = table.Column<int>(type: "integer", nullable: false),
                    aerolinea_id = table.Column<int>(type: "integer", nullable: true),
                    aeropuerto_id = table.Column<int>(type: "integer", nullable: true),
                    fecha_ingreso = table.Column<DateOnly>(type: "date", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_airlines_aerolinea_id",
                        column: x => x.aerolinea_id,
                        principalTable: "airlines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_staff_airports_aeropuerto_id",
                        column: x => x.aeropuerto_id,
                        principalTable: "airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_staff_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staff_staffroles_cargo_id",
                        column: x => x.cargo_id,
                        principalTable: "staffroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    persona_id = table.Column<int>(type: "integer", nullable: true),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_people_persona_id",
                        column: x => x.persona_id,
                        principalTable: "people",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_users_systemroles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "systemroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ruta_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_cabina_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_pasajero_id = table.Column<int>(type: "integer", nullable: false),
                    temporada_id = table.Column<int>(type: "integer", nullable: false),
                    precio_base = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    vigencia_desde = table.Column<DateOnly>(type: "date", nullable: true),
                    vigencia_hasta = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fares_cabintypes_tipo_cabina_id",
                        column: x => x.tipo_cabina_id,
                        principalTable: "cabintypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fares_passengertypes_tipo_pasajero_id",
                        column: x => x.tipo_pasajero_id,
                        principalTable: "passengertypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fares_routes_ruta_id",
                        column: x => x.ruta_id,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fares_seasons_temporada_id",
                        column: x => x.temporada_id,
                        principalTable: "seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_vuelo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    aerolinea_id = table.Column<int>(type: "integer", nullable: false),
                    ruta_id = table.Column<int>(type: "integer", nullable: false),
                    aeronave_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_salida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_llegada_estimada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    capacidad_total = table.Column<int>(type: "integer", nullable: false),
                    asientos_disponibles = table.Column<int>(type: "integer", nullable: false),
                    estado_vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    reprogramado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flights_aircraft_aeronave_id",
                        column: x => x.aeronave_id,
                        principalTable: "aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_airlines_aerolinea_id",
                        column: x => x.aerolinea_id,
                        principalTable: "airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_flightstates_estado_vuelo_id",
                        column: x => x.estado_vuelo_id,
                        principalTable: "flightstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_routes_ruta_id",
                        column: x => x.ruta_id,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "routestops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ruta_id = table.Column<int>(type: "integer", nullable: false),
                    aeropuerto_escala_id = table.Column<int>(type: "integer", nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false),
                    duracion_escala_min = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routestops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_routestops_airports_aeropuerto_escala_id",
                        column: x => x.aeropuerto_escala_id,
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_routestops_routes_ruta_id",
                        column: x => x.ruta_id,
                        principalTable: "routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_reserva = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cliente_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_reserva = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado_reserva_id = table.Column<int>(type: "integer", nullable: false),
                    valor_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    vence_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_clients_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_reservationstatuses_estado_reserva_id",
                        column: x => x.estado_reserva_id,
                        principalTable: "reservationstatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staffavailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    personal_id = table.Column<int>(type: "integer", nullable: false),
                    estado_disponibilidad_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    observacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffavailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staffavailability_availabilitystatuses_estado_disponibilida~",
                        column: x => x.estado_disponibilidad_id,
                        principalTable: "availabilitystatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staffavailability_staff_personal_id",
                        column: x => x.personal_id,
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    iniciada_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cerrada_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ip_origen = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    activa = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sessions_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flightassignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    personal_id = table.Column<int>(type: "integer", nullable: false),
                    rol_vuelo_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightassignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flightassignments_flightroles_rol_vuelo_id",
                        column: x => x.rol_vuelo_id,
                        principalTable: "flightroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flightassignments_flights_vuelo_id",
                        column: x => x.vuelo_id,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flightassignments_staff_personal_id",
                        column: x => x.personal_id,
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flighthistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    estado_anterior_id = table.Column<int>(type: "integer", nullable: false),
                    estado_nuevo_id = table.Column<int>(type: "integer", nullable: false),
                    cambiado_por = table.Column<int>(type: "integer", nullable: true),
                    fecha_cambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    observacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flighthistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flighthistory_flights_vuelo_id",
                        column: x => x.vuelo_id,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flighthistory_flightstates_estado_anterior_id",
                        column: x => x.estado_anterior_id,
                        principalTable: "flightstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flighthistory_flightstates_estado_nuevo_id",
                        column: x => x.estado_nuevo_id,
                        principalTable: "flightstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "flightseats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    codigo_asiento = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    tipo_cabina_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_ubicacion_id = table.Column<int>(type: "integer", nullable: false),
                    esta_ocupado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightseats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flightseats_cabintypes_tipo_cabina_id",
                        column: x => x.tipo_cabina_id,
                        principalTable: "cabintypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flightseats_flights_vuelo_id",
                        column: x => x.vuelo_id,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flightseats_seatlocationtypes_tipo_ubicacion_id",
                        column: x => x.tipo_ubicacion_id,
                        principalTable: "seatlocationtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reserva_id = table.Column<int>(type: "integer", nullable: false),
                    numero_factura = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    impuestos = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_reservations_reserva_id",
                        column: x => x.reserva_id,
                        principalTable: "reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reserva_id = table.Column<int>(type: "integer", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado_pago_id = table.Column<int>(type: "integer", nullable: false),
                    metodo_pago_id = table.Column<int>(type: "integer", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_paymentmethods_metodo_pago_id",
                        column: x => x.metodo_pago_id,
                        principalTable: "paymentmethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payments_paymentstates_estado_pago_id",
                        column: x => x.estado_pago_id,
                        principalTable: "paymentstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payments_reservations_reserva_id",
                        column: x => x.reserva_id,
                        principalTable: "reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservationflights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reserva_id = table.Column<int>(type: "integer", nullable: false),
                    vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    valor_parcial = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationflights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservationflights_flights_vuelo_id",
                        column: x => x.vuelo_id,
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservationflights_reservations_reserva_id",
                        column: x => x.reserva_id,
                        principalTable: "reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservationpassengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reserva_vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    pasajero_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationpassengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservationpassengers_passengers_pasajero_id",
                        column: x => x.pasajero_id,
                        principalTable: "passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservationpassengers_reservationflights_reserva_vuelo_id",
                        column: x => x.reserva_vuelo_id,
                        principalTable: "reservationflights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoiceitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    factura_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_item_id = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    precio_unitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    reserva_pasajero_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoiceitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoiceitems_invoiceitemtypes_tipo_item_id",
                        column: x => x.tipo_item_id,
                        principalTable: "invoiceitemtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoiceitems_invoices_factura_id",
                        column: x => x.factura_id,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoiceitems_reservationpassengers_reserva_pasajero_id",
                        column: x => x.reserva_pasajero_id,
                        principalTable: "reservationpassengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reserva_pasajero_id = table.Column<int>(type: "integer", nullable: false),
                    codigo_tiquete = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado_tiquete_id = table.Column<int>(type: "integer", nullable: false),
                    actualizado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_reservationpassengers_reserva_pasajero_id",
                        column: x => x.reserva_pasajero_id,
                        principalTable: "reservationpassengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_ticketstatuses_estado_tiquete_id",
                        column: x => x.estado_tiquete_id,
                        principalTable: "ticketstatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tiquete_id = table.Column<int>(type: "integer", nullable: false),
                    personal_id = table.Column<int>(type: "integer", nullable: false),
                    asiento_vuelo_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_checkin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado_checkin_id = table.Column<int>(type: "integer", nullable: false),
                    numero_tarjeta_embarque = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    equipaje_bodega = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    peso_equipaje_kg = table.Column<decimal>(type: "numeric(5,2)", nullable: true, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkins_checkinstatuses_estado_checkin_id",
                        column: x => x.estado_checkin_id,
                        principalTable: "checkinstatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkins_flightseats_asiento_vuelo_id",
                        column: x => x.asiento_vuelo_id,
                        principalTable: "flightseats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkins_staff_personal_id",
                        column: x => x.personal_id,
                        principalTable: "staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkins_tickets_tiquete_id",
                        column: x => x.tiquete_id,
                        principalTable: "tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_ciudad_id",
                table: "addresses",
                column: "ciudad_id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_tipo_via_id",
                table: "addresses",
                column: "tipo_via_id");

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_aerolinea_id",
                table: "aircraft",
                column: "aerolinea_id");

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_matricula",
                table: "aircraft",
                column: "matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_modelo_id",
                table: "aircraft",
                column: "modelo_id");

            migrationBuilder.CreateIndex(
                name: "IX_aircraftmaintenance_aeronave_id",
                table: "aircraftmaintenance",
                column: "aeronave_id");

            migrationBuilder.CreateIndex(
                name: "IX_aircraftmaintenance_tipo_mantenimiento_id",
                table: "aircraftmaintenance",
                column: "tipo_mantenimiento_id");

            migrationBuilder.CreateIndex(
                name: "IX_aircraftmodels_fabricante_id",
                table: "aircraftmodels",
                column: "fabricante_id");

            migrationBuilder.CreateIndex(
                name: "IX_airlines_codigo_iata",
                table: "airlines",
                column: "codigo_iata",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airlines_pais_origen_id",
                table: "airlines",
                column: "pais_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_airportairline_aerolinea_id",
                table: "airportairline",
                column: "aerolinea_id");

            migrationBuilder.CreateIndex(
                name: "IX_airportairline_aeropuerto_id_aerolinea_id",
                table: "airportairline",
                columns: new[] { "aeropuerto_id", "aerolinea_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airports_ciudad_id",
                table: "airports",
                column: "ciudad_id");

            migrationBuilder.CreateIndex(
                name: "IX_airports_codigo_iata",
                table: "airports",
                column: "codigo_iata",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airports_codigo_icao",
                table: "airports",
                column: "codigo_icao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_availabilitystatuses_nombre",
                table: "availabilitystatuses",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabinconfiguration_aeronave_id_tipo_cabina_id",
                table: "cabinconfiguration",
                columns: new[] { "aeronave_id", "tipo_cabina_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabinconfiguration_tipo_cabina_id",
                table: "cabinconfiguration",
                column: "tipo_cabina_id");

            migrationBuilder.CreateIndex(
                name: "IX_cabintypes_nombre",
                table: "cabintypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cardissuers_nombre",
                table: "cardissuers",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cardtypes_nombre",
                table: "cardtypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_asiento_vuelo_id",
                table: "checkins",
                column: "asiento_vuelo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_estado_checkin_id",
                table: "checkins",
                column: "estado_checkin_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_numero_tarjeta_embarque",
                table: "checkins",
                column: "numero_tarjeta_embarque",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_personal_id",
                table: "checkins",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_tiquete_id",
                table: "checkins",
                column: "tiquete_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkinstatuses_nombre",
                table: "checkinstatuses",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cities_region_id",
                table: "cities",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_persona_id",
                table: "clients",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_continents_nombre",
                table: "continents",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_codigo_iso",
                table: "countries",
                column: "codigo_iso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_countries_continente_id",
                table: "countries",
                column: "continente_id");

            migrationBuilder.CreateIndex(
                name: "IX_documenttypes_codigo",
                table: "documenttypes",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_emaildomains_dominio",
                table: "emaildomains",
                column: "dominio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fares_ruta_id",
                table: "fares",
                column: "ruta_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_temporada_id",
                table: "fares",
                column: "temporada_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_tipo_cabina_id",
                table: "fares",
                column: "tipo_cabina_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_tipo_pasajero_id",
                table: "fares",
                column: "tipo_pasajero_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightassignments_personal_id",
                table: "flightassignments",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightassignments_rol_vuelo_id",
                table: "flightassignments",
                column: "rol_vuelo_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightassignments_vuelo_id_personal_id",
                table: "flightassignments",
                columns: new[] { "vuelo_id", "personal_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flighthistory_estado_anterior_id",
                table: "flighthistory",
                column: "estado_anterior_id");

            migrationBuilder.CreateIndex(
                name: "IX_flighthistory_estado_nuevo_id",
                table: "flighthistory",
                column: "estado_nuevo_id");

            migrationBuilder.CreateIndex(
                name: "IX_flighthistory_vuelo_id",
                table: "flighthistory",
                column: "vuelo_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightroles_nombre",
                table: "flightroles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_aerolinea_id",
                table: "flights",
                column: "aerolinea_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_aeronave_id",
                table: "flights",
                column: "aeronave_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_codigo_vuelo",
                table: "flights",
                column: "codigo_vuelo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_estado_vuelo_id",
                table: "flights",
                column: "estado_vuelo_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_ruta_id",
                table: "flights",
                column: "ruta_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightseats_tipo_cabina_id",
                table: "flightseats",
                column: "tipo_cabina_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightseats_tipo_ubicacion_id",
                table: "flightseats",
                column: "tipo_ubicacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightseats_vuelo_id_codigo_asiento",
                table: "flightseats",
                columns: new[] { "vuelo_id", "codigo_asiento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flightstates_nombre",
                table: "flightstates",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flightstatustransitions_estado_destino_id",
                table: "flightstatustransitions",
                column: "estado_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_flightstatustransitions_estado_origen_id_estado_destino_id",
                table: "flightstatustransitions",
                columns: new[] { "estado_origen_id", "estado_destino_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoiceitems_factura_id",
                table: "invoiceitems",
                column: "factura_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoiceitems_reserva_pasajero_id",
                table: "invoiceitems",
                column: "reserva_pasajero_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoiceitems_tipo_item_id",
                table: "invoiceitems",
                column: "tipo_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoiceitemtypes_nombre",
                table: "invoiceitemtypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_numero_factura",
                table: "invoices",
                column: "numero_factura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_reserva_id",
                table: "invoices",
                column: "reserva_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_maintenancetypes_nombre",
                table: "maintenancetypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passengers_persona_id",
                table: "passengers",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passengers_tipo_pasajero_id",
                table: "passengers",
                column: "tipo_pasajero_id");

            migrationBuilder.CreateIndex(
                name: "IX_passengertypes_nombre",
                table: "passengertypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_emisor_tarjeta_id",
                table: "paymentmethods",
                column: "emisor_tarjeta_id");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_nombre_comercial",
                table: "paymentmethods",
                column: "nombre_comercial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_tipo_medio_pago_id",
                table: "paymentmethods",
                column: "tipo_medio_pago_id");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethods_tipo_tarjeta_id",
                table: "paymentmethods",
                column: "tipo_tarjeta_id");

            migrationBuilder.CreateIndex(
                name: "IX_paymentmethodtypes_nombre",
                table: "paymentmethodtypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_estado_pago_id",
                table: "payments",
                column: "estado_pago_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_metodo_pago_id",
                table: "payments",
                column: "metodo_pago_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_reserva_id",
                table: "payments",
                column: "reserva_id");

            migrationBuilder.CreateIndex(
                name: "IX_paymentstates_nombre",
                table: "paymentstates",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_people_direccion_id",
                table: "people",
                column: "direccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_tipo_documento_id_numero_documento",
                table: "people",
                columns: new[] { "tipo_documento_id", "numero_documento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_nombre",
                table: "permissions",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personemails_dominio_email_id",
                table: "personemails",
                column: "dominio_email_id");

            migrationBuilder.CreateIndex(
                name: "IX_personemails_persona_id",
                table: "personemails",
                column: "persona_id");

            migrationBuilder.CreateIndex(
                name: "IX_personphones_codigo_telefono_id",
                table: "personphones",
                column: "codigo_telefono_id");

            migrationBuilder.CreateIndex(
                name: "IX_personphones_persona_id",
                table: "personphones",
                column: "persona_id");

            migrationBuilder.CreateIndex(
                name: "IX_phonecodes_codigo_pais",
                table: "phonecodes",
                column: "codigo_pais",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_regions_pais_id",
                table: "regions",
                column: "pais_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservationflights_reserva_id_vuelo_id",
                table: "reservationflights",
                columns: new[] { "reserva_id", "vuelo_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservationflights_vuelo_id",
                table: "reservationflights",
                column: "vuelo_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservationpassengers_pasajero_id",
                table: "reservationpassengers",
                column: "pasajero_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservationpassengers_reserva_vuelo_id_pasajero_id",
                table: "reservationpassengers",
                columns: new[] { "reserva_vuelo_id", "pasajero_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_cliente_id",
                table: "reservations",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_codigo_reserva",
                table: "reservations",
                column: "codigo_reserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_estado_reserva_id",
                table: "reservations",
                column: "estado_reserva_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservationstatuses_nombre",
                table: "reservationstatuses",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservationstatustransitions_estado_destino_id",
                table: "reservationstatustransitions",
                column: "estado_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservationstatustransitions_estado_origen_id_estado_destin~",
                table: "reservationstatustransitions",
                columns: new[] { "estado_origen_id", "estado_destino_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rolepermissions_permiso_id",
                table: "rolepermissions",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "IX_rolepermissions_rol_id_permiso_id",
                table: "rolepermissions",
                columns: new[] { "rol_id", "permiso_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routes_aeropuerto_destino_id",
                table: "routes",
                column: "aeropuerto_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_routes_aeropuerto_origen_id_aeropuerto_destino_id",
                table: "routes",
                columns: new[] { "aeropuerto_origen_id", "aeropuerto_destino_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_routestops_aeropuerto_escala_id",
                table: "routestops",
                column: "aeropuerto_escala_id");

            migrationBuilder.CreateIndex(
                name: "IX_routestops_ruta_id_orden",
                table: "routestops",
                columns: new[] { "ruta_id", "orden" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_seasons_nombre",
                table: "seasons",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_seatlocationtypes_nombre",
                table: "seatlocationtypes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sessions_usuario_id",
                table: "sessions",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_aerolinea_id",
                table: "staff",
                column: "aerolinea_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_aeropuerto_id",
                table: "staff",
                column: "aeropuerto_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_cargo_id",
                table: "staff",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_persona_id",
                table: "staff",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_staffavailability_estado_disponibilidad_id",
                table: "staffavailability",
                column: "estado_disponibilidad_id");

            migrationBuilder.CreateIndex(
                name: "IX_staffavailability_personal_id",
                table: "staffavailability",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_staffroles_nombre",
                table: "staffroles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_systemroles_nombre",
                table: "systemroles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_codigo_tiquete",
                table: "tickets",
                column: "codigo_tiquete",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_estado_tiquete_id",
                table: "tickets",
                column: "estado_tiquete_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_reserva_pasajero_id",
                table: "tickets",
                column: "reserva_pasajero_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticketstatuses_nombre",
                table: "ticketstatuses",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_persona_id",
                table: "users",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_rol_id",
                table: "users",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aircraftmaintenance");

            migrationBuilder.DropTable(
                name: "airportairline");

            migrationBuilder.DropTable(
                name: "cabinconfiguration");

            migrationBuilder.DropTable(
                name: "checkins");

            migrationBuilder.DropTable(
                name: "fares");

            migrationBuilder.DropTable(
                name: "flightassignments");

            migrationBuilder.DropTable(
                name: "flighthistory");

            migrationBuilder.DropTable(
                name: "flightstatustransitions");

            migrationBuilder.DropTable(
                name: "invoiceitems");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "personemails");

            migrationBuilder.DropTable(
                name: "personphones");

            migrationBuilder.DropTable(
                name: "reservationstatustransitions");

            migrationBuilder.DropTable(
                name: "rolepermissions");

            migrationBuilder.DropTable(
                name: "routestops");

            migrationBuilder.DropTable(
                name: "sessions");

            migrationBuilder.DropTable(
                name: "staffavailability");

            migrationBuilder.DropTable(
                name: "maintenancetypes");

            migrationBuilder.DropTable(
                name: "checkinstatuses");

            migrationBuilder.DropTable(
                name: "flightseats");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "flightroles");

            migrationBuilder.DropTable(
                name: "invoiceitemtypes");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "paymentmethods");

            migrationBuilder.DropTable(
                name: "paymentstates");

            migrationBuilder.DropTable(
                name: "emaildomains");

            migrationBuilder.DropTable(
                name: "phonecodes");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "availabilitystatuses");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "cabintypes");

            migrationBuilder.DropTable(
                name: "seatlocationtypes");

            migrationBuilder.DropTable(
                name: "reservationpassengers");

            migrationBuilder.DropTable(
                name: "ticketstatuses");

            migrationBuilder.DropTable(
                name: "cardissuers");

            migrationBuilder.DropTable(
                name: "cardtypes");

            migrationBuilder.DropTable(
                name: "paymentmethodtypes");

            migrationBuilder.DropTable(
                name: "systemroles");

            migrationBuilder.DropTable(
                name: "staffroles");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "reservationflights");

            migrationBuilder.DropTable(
                name: "passengertypes");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "aircraft");

            migrationBuilder.DropTable(
                name: "flightstates");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "reservationstatuses");

            migrationBuilder.DropTable(
                name: "aircraftmodels");

            migrationBuilder.DropTable(
                name: "airlines");

            migrationBuilder.DropTable(
                name: "airports");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "aircraftmanufacturers");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "documenttypes");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "roadtypes");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "continents");
        }
    }
}

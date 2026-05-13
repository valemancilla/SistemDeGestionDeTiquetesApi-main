using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignCanonicalAirlinesDbIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_fares_route_id",
                schema: "airlinesdb",
                table: "fares");

            migrationBuilder.DropIndex(
                name: "IX_airlines_iata_code",
                schema: "airlinesdb",
                table: "airlines");

            migrationBuilder.RenameIndex(
                name: "IX_reservation_status_transitions_source_status_id_target_stat~",
                schema: "airlinesdb",
                table: "reservation_status_transitions",
                newName: "IX_reservation_status_transitions_source_status_id_target_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_fares_route_id_cabin_type_id_passenger_type_id_season_id",
                schema: "airlinesdb",
                table: "fares",
                columns: new[] { "route_id", "cabin_type_id", "passenger_type_id", "season_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_fares_route_id_cabin_type_id_passenger_type_id_season_id",
                schema: "airlinesdb",
                table: "fares");

            migrationBuilder.RenameIndex(
                name: "IX_reservation_status_transitions_source_status_id_target_status_id",
                schema: "airlinesdb",
                table: "reservation_status_transitions",
                newName: "IX_reservation_status_transitions_source_status_id_target_stat~");

            migrationBuilder.CreateIndex(
                name: "IX_fares_route_id",
                schema: "airlinesdb",
                table: "fares",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_airlines_iata_code",
                schema: "airlinesdb",
                table: "airlines",
                column: "iata_code",
                unique: true);
        }
    }
}

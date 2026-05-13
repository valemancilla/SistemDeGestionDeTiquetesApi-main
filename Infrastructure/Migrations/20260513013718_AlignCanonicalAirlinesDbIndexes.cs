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
            // Idempotente: el DDL canónico puede no tener IX_fares_route_id / IX_airlines_iata_code,
            // o ya tener el índice compuesto y nombres finales.
            migrationBuilder.Sql(
                """
                DROP INDEX IF EXISTS airlinesdb."IX_fares_route_id";
                DROP INDEX IF EXISTS airlinesdb.ix_fares_route_id;
                DROP INDEX IF EXISTS airlinesdb."IX_airlines_iata_code";
                DROP INDEX IF EXISTS airlinesdb.ix_airlines_iata_code;

                DO $$
                DECLARE
                  old_idx text;
                  new_idx text := 'IX_reservation_status_transitions_source_status_id_target_status_id';
                BEGIN
                  IF to_regclass('airlinesdb.reservation_status_transitions') IS NOT NULL THEN
                    IF NOT EXISTS (
                      SELECT 1 FROM pg_indexes
                      WHERE schemaname = 'airlinesdb'
                        AND tablename = 'reservation_status_transitions'
                        AND LOWER(indexname) = LOWER(new_idx)
                    ) THEN
                      SELECT indexname INTO old_idx
                      FROM pg_indexes
                      WHERE schemaname = 'airlinesdb'
                        AND tablename = 'reservation_status_transitions'
                        AND LOWER(indexname) LIKE 'ix_reservation_status_transitions_source_status%'
                        AND LOWER(indexname) <> LOWER(new_idx)
                      ORDER BY length(indexname) DESC
                      LIMIT 1;

                      IF old_idx IS NOT NULL THEN
                        EXECUTE format('ALTER INDEX airlinesdb.%I RENAME TO %I', old_idx, new_idx);
                      END IF;
                    END IF;
                  END IF;

                  IF to_regclass('airlinesdb.fares') IS NOT NULL THEN
                    CREATE INDEX IF NOT EXISTS "IX_fares_route_id_cabin_type_id_passenger_type_id_season_id"
                      ON airlinesdb.fares (route_id, cabin_type_id, passenger_type_id, season_id);
                  END IF;
                END $$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DROP INDEX IF EXISTS airlinesdb."IX_fares_route_id_cabin_type_id_passenger_type_id_season_id";
                DROP INDEX IF EXISTS airlinesdb.ix_fares_route_id_cabin_type_id_passenger_type_id_season_id;

                DO $$
                DECLARE
                  cur_idx text;
                  old_trunc text := 'IX_reservation_status_transitions_source_status_id_target_stat~';
                BEGIN
                  IF to_regclass('airlinesdb.reservation_status_transitions') IS NOT NULL THEN
                    SELECT indexname INTO cur_idx
                    FROM pg_indexes
                    WHERE schemaname = 'airlinesdb'
                      AND tablename = 'reservation_status_transitions'
                      AND LOWER(indexname) = LOWER('IX_reservation_status_transitions_source_status_id_target_status_id')
                    LIMIT 1;

                    IF cur_idx IS NOT NULL
                       AND NOT EXISTS (
                         SELECT 1 FROM pg_indexes
                         WHERE schemaname = 'airlinesdb'
                           AND tablename = 'reservation_status_transitions'
                           AND indexname = old_trunc
                       ) THEN
                      EXECUTE format('ALTER INDEX airlinesdb.%I RENAME TO %I', cur_idx, old_trunc);
                    END IF;
                  END IF;

                  IF to_regclass('airlinesdb.fares') IS NOT NULL THEN
                    CREATE INDEX IF NOT EXISTS "IX_fares_route_id"
                      ON airlinesdb.fares (route_id);
                  END IF;

                  IF to_regclass('airlinesdb.airlines') IS NOT NULL THEN
                    CREATE UNIQUE INDEX IF NOT EXISTS "IX_airlines_iata_code"
                      ON airlinesdb.airlines (iata_code);
                  END IF;
                END $$;
                """);
        }
    }
}

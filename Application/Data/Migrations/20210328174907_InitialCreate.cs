using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Application.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "covid_observations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'utc'"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'utc'"),
                    observation_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'utc'"),
                    province_state = table.Column<string>(type: "varchar", maxLength: 256, nullable: true),
                    country_region = table.Column<string>(type: "varchar", maxLength: 256, nullable: true),
                    confirmed = table.Column<int>(type: "integer", nullable: false),
                    deaths = table.Column<int>(type: "integer", nullable: false),
                    recovered = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_covid_observations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "covid_observations");
        }
    }
}

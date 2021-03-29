using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Data.Migrations
{
    public partial class UpdatedPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_covid_observations",
                table: "covid_observations");

            migrationBuilder.AddPrimaryKey(
                name: "pk_orders_id",
                table: "covid_observations",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_orders_id",
                table: "covid_observations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_covid_observations",
                table: "covid_observations",
                column: "id");
        }
    }
}

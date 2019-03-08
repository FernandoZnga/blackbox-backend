using Microsoft.EntityFrameworkCore.Migrations;

namespace Blackbox.Server.Migrations
{
    public partial class NewFieldAtmId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AtmId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AtmId",
                table: "__TextLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtmId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AtmId",
                table: "__TextLogs");
        }
    }
}

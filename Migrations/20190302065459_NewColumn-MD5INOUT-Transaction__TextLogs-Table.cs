using Microsoft.EntityFrameworkCore.Migrations;

namespace Blackbox.Server.Migrations
{
    public partial class NewColumnMD5INOUTTransaction__TextLogsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Md5IN",
                table: "__TextLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Md5OUT",
                table: "__TextLogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transaction",
                table: "__TextLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Md5IN",
                table: "__TextLogs");

            migrationBuilder.DropColumn(
                name: "Md5OUT",
                table: "__TextLogs");

            migrationBuilder.DropColumn(
                name: "Transaction",
                table: "__TextLogs");
        }
    }
}

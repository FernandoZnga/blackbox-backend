using Microsoft.EntityFrameworkCore.Migrations;

namespace Blackbox.Server.Migrations
{
    public partial class NewFieldAccountTypeNameTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountTypeName",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountTypeName",
                table: "Transactions");
        }
    }
}

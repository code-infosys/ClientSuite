using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSuite.Data.Migrations
{
    public partial class UpdateFieldOfInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuoteNumber",
                schema: "Quote",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "Quote",
                table: "Invoice",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "Quote",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "QuoteNumber",
                schema: "Quote",
                table: "Invoice",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

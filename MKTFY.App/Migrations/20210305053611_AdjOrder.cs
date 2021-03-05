using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class AdjOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Charges",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charges",
                table: "Orders");
        }
    }
}

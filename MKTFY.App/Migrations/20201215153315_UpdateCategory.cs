using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

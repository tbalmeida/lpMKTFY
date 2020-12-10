using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class AddCityToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");
        }
    }
}

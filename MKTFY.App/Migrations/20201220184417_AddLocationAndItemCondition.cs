using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MKTFY.App.Migrations
{
    public partial class AddLocationAndItemCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemConditionId",
                table: "Listings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Listings",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "itemConditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemConditions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ItemConditionId",
                table: "Listings",
                column: "ItemConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_itemConditions_ItemConditionId",
                table: "Listings",
                column: "ItemConditionId",
                principalTable: "itemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_itemConditions_ItemConditionId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "itemConditions");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ItemConditionId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ItemConditionId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Listings");
        }
    }
}

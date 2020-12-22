using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class FixItemCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_itemConditions_ItemConditionId",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemConditions",
                table: "itemConditions");

            migrationBuilder.RenameTable(
                name: "itemConditions",
                newName: "ItemConditions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemConditions",
                table: "ItemConditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_ItemConditions_ItemConditionId",
                table: "Listings",
                column: "ItemConditionId",
                principalTable: "ItemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_ItemConditions_ItemConditionId",
                table: "Listings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemConditions",
                table: "ItemConditions");

            migrationBuilder.RenameTable(
                name: "ItemConditions",
                newName: "itemConditions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemConditions",
                table: "itemConditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_itemConditions_ItemConditionId",
                table: "Listings",
                column: "ItemConditionId",
                principalTable: "itemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

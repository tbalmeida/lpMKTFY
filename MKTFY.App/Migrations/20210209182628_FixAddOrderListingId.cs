using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class FixAddOrderListingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListingId",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ListingId",
                table: "Orders",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Listings_ListingId",
                table: "Orders",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Listings_ListingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ListingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Orders");
        }
    }
}

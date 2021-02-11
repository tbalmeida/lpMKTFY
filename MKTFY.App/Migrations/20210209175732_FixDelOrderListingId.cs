using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class FixDelOrderListingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Listings_ListingId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ListingId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ListingId1",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ListingId1",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ListingId1",
                table: "Orders",
                column: "ListingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Listings_ListingId1",
                table: "Orders",
                column: "ListingId1",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

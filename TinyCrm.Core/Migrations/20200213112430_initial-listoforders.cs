using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrm.Core.Migrations
{
    public partial class initiallistoforders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "core",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Datetime",
                schema: "core",
                table: "Order");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDatetime",
                schema: "core",
                table: "Order",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "core",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                schema: "core",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "core",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "core",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order",
                column: "CustomerId",
                principalSchema: "core",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                schema: "core",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreateDatetime",
                schema: "core",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "core",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                schema: "core",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "core",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "core",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Datetime",
                schema: "core",
                table: "Order",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}

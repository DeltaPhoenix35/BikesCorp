using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesTest.Migrations
{
    public partial class subscriptions_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 1,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 2,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 1,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 2,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}

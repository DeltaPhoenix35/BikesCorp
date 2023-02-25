using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesTest.Migrations
{
    public partial class subscriptions_17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPremium",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 1,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 2,
                column: "aquisutionDate",
                value: new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPremium",
                table: "SubscriptionPlans");

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
    }
}

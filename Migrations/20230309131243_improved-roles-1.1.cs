using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesTest.Migrations
{
    public partial class improvedroles11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "id", "isCurrentlyLogged", "isSuspended", "user_id" },
                values: new object[] { 3, false, false, 4 });

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 1,
                column: "aquisutionDate",
                value: new DateTime(2023, 3, 9, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 2,
                column: "aquisutionDate",
                value: new DateTime(2023, 3, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 1,
                column: "aquisutionDate",
                value: new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Bicycles",
                keyColumn: "id",
                keyValue: 2,
                column: "aquisutionDate",
                value: new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}

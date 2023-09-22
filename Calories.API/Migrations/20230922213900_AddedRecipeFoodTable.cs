using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calories.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecipeFoodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 9, 22, 16, 39, 0, 81, DateTimeKind.Local).AddTicks(2897));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 9, 21, 19, 4, 46, 949, DateTimeKind.Local).AddTicks(1829));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calories.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "Id", "BirthDate", "CalorieId", "DailyCalories", "Gender", "Height", "RecipeFoodId", "UserId", "Weight" },
                values: new object[] { 1, new DateTime(2023, 9, 21, 19, 4, 46, 949, DateTimeKind.Local).AddTicks(1829), 1, 1, "Male", 100.0, 1, "Test", 100.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

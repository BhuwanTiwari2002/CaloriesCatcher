using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calories.API.Migrations
{
    /// <inheritdoc />
    public partial class addingUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "Id", "BirthDate", "CalorieId", "DailyCalories", "Gender", "Height", "RecipeFoodId", "UserId", "Weight" },
                values: new object[] { 1, new DateTime(2023, 9, 24, 11, 41, 23, 434, DateTimeKind.Local).AddTicks(6680), 1, 1, "Male", 100.0, 1, "Test", 100.0 });
        }
    }
}

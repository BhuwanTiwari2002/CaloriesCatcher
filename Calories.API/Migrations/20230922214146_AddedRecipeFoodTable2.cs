using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calories.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecipeFoodTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeFoods",
                columns: table => new
                {
                    RecipeFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFoods", x => x.RecipeFoodId);
                });

            migrationBuilder.UpdateData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 9, 22, 16, 41, 46, 365, DateTimeKind.Local).AddTicks(1402));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeFoods");

            migrationBuilder.UpdateData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 9, 22, 16, 39, 0, 81, DateTimeKind.Local).AddTicks(2897));
        }
    }
}

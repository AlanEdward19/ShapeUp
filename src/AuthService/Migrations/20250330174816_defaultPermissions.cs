using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class defaultPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "Theme", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3c2ca824-eaf0-4dd1-9578-3130c220fce7"), 1, new DateTime(2025, 3, 30, 14, 48, 16, 132, DateTimeKind.Local).AddTicks(3287), "mealPlan", new DateTime(2025, 3, 30, 14, 48, 16, 133, DateTimeKind.Local).AddTicks(221) },
                    { new Guid("af0f048b-a0db-4316-affa-4d16319a5159"), 1, new DateTime(2025, 3, 30, 14, 48, 16, 133, DateTimeKind.Local).AddTicks(247), "workoutPlan", new DateTime(2025, 3, 30, 14, 48, 16, 133, DateTimeKind.Local).AddTicks(248) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3c2ca824-eaf0-4dd1-9578-3130c220fce7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("af0f048b-a0db-4316-affa-4d16319a5159"));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class defaultPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "Theme", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0c9c9154-2479-406c-8bd2-e15a22c8b31e"), 0, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "permission", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a86b7651-2aef-4f55-9c85-7b1f6d486f14"), 1, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "permission", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3856d63-8c6d-41c7-bfb0-4a327662a6dc"), 2, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "permission", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e0dcbf9b-1d24-413a-8839-5d70f9ace22a"), 3, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "permission", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0c9c9154-2479-406c-8bd2-e15a22c8b31e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a86b7651-2aef-4f55-9c85-7b1f6d486f14"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d3856d63-8c6d-41c7-bfb0-4a327662a6dc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e0dcbf9b-1d24-413a-8839-5d70f9ace22a"));
        }
    }
}

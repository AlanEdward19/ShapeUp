using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfessionalManagementService.Migrations
{
    /// <inheritdoc />
    public partial class NewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "ClientServicePlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelledBy",
                table: "ClientServicePlans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "ClientServicePlans");

            migrationBuilder.DropColumn(
                name: "CancelledBy",
                table: "ClientServicePlans");
        }
    }
}

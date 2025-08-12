using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfessionalManagementService.Migrations
{
    /// <inheritdoc />
    public partial class AddServicePlanType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ServicePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ServicePlans");
        }
    }
}

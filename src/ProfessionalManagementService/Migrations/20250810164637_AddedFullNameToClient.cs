using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfessionalManagementService.Migrations
{
    /// <inheritdoc />
    public partial class AddedFullNameToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");
        }
    }
}

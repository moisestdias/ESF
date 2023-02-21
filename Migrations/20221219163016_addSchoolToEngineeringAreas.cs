using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpmDashboard.Migrations
{
    /// <inheritdoc />
    public partial class addSchoolToEngineeringAreas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "EngineeringAreas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "School",
                table: "EngineeringAreas");
        }
    }
}

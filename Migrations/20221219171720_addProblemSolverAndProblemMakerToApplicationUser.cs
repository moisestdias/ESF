using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpmDashboard.Migrations
{
    /// <inheritdoc />
    public partial class addProblemSolverAndProblemMakerToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "ProblemSolver",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "ProblemMaker",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "ProblemSolver");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "ProblemMaker");
        }
    }
}

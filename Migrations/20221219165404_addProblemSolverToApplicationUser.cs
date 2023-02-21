using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpmDashboard.Migrations
{
    /// <inheritdoc />
    public partial class addProblemSolverToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ProblemSolver_ProblemSolverid",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ProblemSolverid",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ProblemSolverid",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProblemSolver",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemSolver_ApplicationUserId",
                table: "ProblemSolver",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemSolver_ApplicationUser_ApplicationUserId",
                table: "ProblemSolver",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemSolver_ApplicationUser_ApplicationUserId",
                table: "ProblemSolver");

            migrationBuilder.DropIndex(
                name: "IX_ProblemSolver_ApplicationUserId",
                table: "ProblemSolver");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProblemSolver");

            migrationBuilder.AddColumn<int>(
                name: "ProblemSolverid",
                table: "ApplicationUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ProblemSolverid",
                table: "ApplicationUser",
                column: "ProblemSolverid");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_ProblemSolver_ProblemSolverid",
                table: "ApplicationUser",
                column: "ProblemSolverid",
                principalTable: "ProblemSolver",
                principalColumn: "id");
        }
    }
}

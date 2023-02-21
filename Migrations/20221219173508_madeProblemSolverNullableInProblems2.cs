using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpmDashboard.Migrations
{
    /// <inheritdoc />
    public partial class madeProblemSolverNullableInProblems2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ProblemMaker_ProblemMaker_id",
                table: "Problem");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ProblemSolver_ProblemSolver_id",
                table: "Problem");

            migrationBuilder.RenameColumn(
                name: "ProblemSolver_id",
                table: "Problem",
                newName: "ProblemSolverid");

            migrationBuilder.RenameColumn(
                name: "ProblemMaker_id",
                table: "Problem",
                newName: "ProblemMakerid");

            migrationBuilder.RenameIndex(
                name: "IX_Problem_ProblemSolver_id",
                table: "Problem",
                newName: "IX_Problem_ProblemSolverid");

            migrationBuilder.RenameIndex(
                name: "IX_Problem_ProblemMaker_id",
                table: "Problem",
                newName: "IX_Problem_ProblemMakerid");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemSolverid",
                table: "Problem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemMakerid",
                table: "Problem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ProblemMaker_ProblemMakerid",
                table: "Problem",
                column: "ProblemMakerid",
                principalTable: "ProblemMaker",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ProblemSolver_ProblemSolverid",
                table: "Problem",
                column: "ProblemSolverid",
                principalTable: "ProblemSolver",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ProblemMaker_ProblemMakerid",
                table: "Problem");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ProblemSolver_ProblemSolverid",
                table: "Problem");

            migrationBuilder.RenameColumn(
                name: "ProblemSolverid",
                table: "Problem",
                newName: "ProblemSolver_id");

            migrationBuilder.RenameColumn(
                name: "ProblemMakerid",
                table: "Problem",
                newName: "ProblemMaker_id");

            migrationBuilder.RenameIndex(
                name: "IX_Problem_ProblemSolverid",
                table: "Problem",
                newName: "IX_Problem_ProblemSolver_id");

            migrationBuilder.RenameIndex(
                name: "IX_Problem_ProblemMakerid",
                table: "Problem",
                newName: "IX_Problem_ProblemMaker_id");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemSolver_id",
                table: "Problem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProblemMaker_id",
                table: "Problem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ProblemMaker_ProblemMaker_id",
                table: "Problem",
                column: "ProblemMaker_id",
                principalTable: "ProblemMaker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ProblemSolver_ProblemSolver_id",
                table: "Problem",
                column: "ProblemSolver_id",
                principalTable: "ProblemSolver",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

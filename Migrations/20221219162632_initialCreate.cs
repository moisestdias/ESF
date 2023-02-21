using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpmDashboard.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EngineeringAreas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    area = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringAreas", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProblemMaker",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    about = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: true),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemMaker", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProblemSolver",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profession = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EngineeringAreasid = table.Column<int>(name: "EngineeringAreas_id", type: "int", nullable: true),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: true),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemSolver", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProblemSolver_EngineeringAreas_EngineeringAreas_id",
                        column: x => x.EngineeringAreasid,
                        principalTable: "EngineeringAreas",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createtime = table.Column<DateTime>(name: "create_time", type: "datetime(6)", nullable: true),
                    updatetime = table.Column<DateTime>(name: "update_time", type: "datetime(6)", nullable: true),
                    funding = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ProblemMakerid = table.Column<int>(name: "ProblemMaker_id", type: "int", nullable: false),
                    ProblemSolverid = table.Column<int>(name: "ProblemSolver_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.id);
                    table.ForeignKey(
                        name: "FK_Problem_ProblemMaker_ProblemMaker_id",
                        column: x => x.ProblemMakerid,
                        principalTable: "ProblemMaker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Problem_ProblemSolver_ProblemSolver_id",
                        column: x => x.ProblemSolverid,
                        principalTable: "ProblemSolver",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_ProblemMaker_id",
                table: "Problem",
                column: "ProblemMaker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_ProblemSolver_id",
                table: "Problem",
                column: "ProblemSolver_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemSolver_EngineeringAreas_id",
                table: "ProblemSolver",
                column: "EngineeringAreas_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "ProblemMaker");

            migrationBuilder.DropTable(
                name: "ProblemSolver");

            migrationBuilder.DropTable(
                name: "EngineeringAreas");
        }
    }
}

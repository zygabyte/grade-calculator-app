using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class ChangeSessionToSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionCourses_Sessions_SessionId",
                table: "SessionCourses");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionCourses_SessionId",
                table: "SessionCourses");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Courses",
                newName: "SessionSemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SessionId",
                table: "Courses",
                newName: "IX_Courses_SessionSemesterId");

            migrationBuilder.AddColumn<long>(
                name: "SessionSemesterId",
                table: "SessionCourses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SessionSemesters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SemesterId = table.Column<long>(nullable: false),
                    SemesterStartDate = table.Column<DateTime>(nullable: false),
                    SemesterEndDate = table.Column<DateTime>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSemesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSemesters_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionCourses_SessionSemesterId",
                table: "SessionCourses",
                column: "SessionSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSemesters_SemesterId",
                table: "SessionSemesters",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionSemesters_SessionSemesterId",
                table: "Courses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionSemesters_SessionSemesterId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.DropTable(
                name: "SessionSemesters");

            migrationBuilder.DropIndex(
                name: "IX_SessionCourses_SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.DropColumn(
                name: "SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.RenameColumn(
                name: "SessionSemesterId",
                table: "Courses",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SessionSemesterId",
                table: "Courses",
                newName: "IX_Courses_SessionId");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SemesterEndDate = table.Column<DateTime>(nullable: false),
                    SemesterId = table.Column<long>(nullable: false),
                    SemesterStartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionCourses_SessionId",
                table: "SessionCourses",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SemesterId",
                table: "Sessions",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCourses_Sessions_SessionId",
                table: "SessionCourses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

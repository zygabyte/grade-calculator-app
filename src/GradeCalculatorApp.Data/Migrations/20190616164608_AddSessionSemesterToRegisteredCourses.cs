using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class AddSessionSemesterToRegisteredCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SessionSemesterId",
                table: "RegisteredCourses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "RegisteredCourseGrades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegisteredCourseId = table.Column<long>(nullable: false),
                    Attendance = table.Column<int>(nullable: false),
                    Quiz1 = table.Column<int>(nullable: false),
                    Quiz2 = table.Column<int>(nullable: false),
                    Assignment1 = table.Column<int>(nullable: false),
                    Assignment2 = table.Column<int>(nullable: false),
                    MidSemester = table.Column<int>(nullable: false),
                    Project = table.Column<int>(nullable: false),
                    Exam = table.Column<int>(nullable: false),
                    TotalCa = table.Column<int>(nullable: false),
                    FinalScore = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCourseGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredCourseGrades_RegisteredCourses_RegisteredCourseId",
                        column: x => x.RegisteredCourseId,
                        principalTable: "RegisteredCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCourses_SessionSemesterId",
                table: "RegisteredCourses",
                column: "SessionSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCourseGrades_RegisteredCourseId",
                table: "RegisteredCourseGrades",
                column: "RegisteredCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredCourses_SessionSemesters_SessionSemesterId",
                table: "RegisteredCourses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredCourses_SessionSemesters_SessionSemesterId",
                table: "RegisteredCourses");

            migrationBuilder.DropTable(
                name: "RegisteredCourseGrades");

            migrationBuilder.DropIndex(
                name: "IX_RegisteredCourses_SessionSemesterId",
                table: "RegisteredCourses");

            migrationBuilder.DropColumn(
                name: "SessionSemesterId",
                table: "RegisteredCourses");
        }
    }
}

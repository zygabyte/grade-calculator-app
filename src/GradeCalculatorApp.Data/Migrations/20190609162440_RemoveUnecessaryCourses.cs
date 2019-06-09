using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class RemoveUnecessaryCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_LecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Programmes_ProgrammeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionSemesters_SessionSemesterId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LecturerId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProgrammeId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SessionSemesterId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProgrammeId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SessionSemesterId",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LecturerId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProgrammeId",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SessionSemesterId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LecturerId",
                table: "Courses",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgrammeId",
                table: "Courses",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SessionSemesterId",
                table: "Courses",
                column: "SessionSemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_LecturerId",
                table: "Courses",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Programmes_ProgrammeId",
                table: "Courses",
                column: "ProgrammeId",
                principalTable: "Programmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionSemesters_SessionSemesterId",
                table: "Courses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

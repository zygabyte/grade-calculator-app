using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class RefactorSessionSemesterContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionCourses_SessionSemesterCourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionCourses",
                table: "SessionCourses");

            migrationBuilder.RenameTable(
                name: "SessionCourses",
                newName: "SessionSemesterCourses");

            migrationBuilder.RenameIndex(
                name: "IX_SessionCourses_SessionSemesterId",
                table: "SessionSemesterCourses",
                newName: "IX_SessionSemesterCourses_SessionSemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionSemesterCourses",
                table: "SessionSemesterCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionSemesterCourses_SessionSemesterCourseId",
                table: "Courses",
                column: "SessionSemesterCourseId",
                principalTable: "SessionSemesterCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSemesterCourses_SessionSemesters_SessionSemesterId",
                table: "SessionSemesterCourses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionSemesterCourses_SessionSemesterCourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSemesterCourses_SessionSemesters_SessionSemesterId",
                table: "SessionSemesterCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionSemesterCourses",
                table: "SessionSemesterCourses");

            migrationBuilder.RenameTable(
                name: "SessionSemesterCourses",
                newName: "SessionCourses");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSemesterCourses_SessionSemesterId",
                table: "SessionCourses",
                newName: "IX_SessionCourses_SessionSemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionCourses",
                table: "SessionCourses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionCourses_SessionSemesterCourseId",
                table: "Courses",
                column: "SessionSemesterCourseId",
                principalTable: "SessionCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses",
                column: "SessionSemesterId",
                principalTable: "SessionSemesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class RefactorSessionSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionCourses_SessionCourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SessionCourses");

            migrationBuilder.RenameColumn(
                name: "SessionCourseId",
                table: "Courses",
                newName: "SessionSemesterCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SessionCourseId",
                table: "Courses",
                newName: "IX_Courses_SessionSemesterCourseId");

            migrationBuilder.AlterColumn<long>(
                name: "SessionSemesterId",
                table: "SessionCourses",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionCourses_SessionSemesterCourseId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionCourses_SessionSemesters_SessionSemesterId",
                table: "SessionCourses");

            migrationBuilder.RenameColumn(
                name: "SessionSemesterCourseId",
                table: "Courses",
                newName: "SessionCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SessionSemesterCourseId",
                table: "Courses",
                newName: "IX_Courses_SessionCourseId");

            migrationBuilder.AlterColumn<long>(
                name: "SessionSemesterId",
                table: "SessionCourses",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "SessionId",
                table: "SessionCourses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionCourses_SessionCourseId",
                table: "Courses",
                column: "SessionCourseId",
                principalTable: "SessionCourses",
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
    }
}

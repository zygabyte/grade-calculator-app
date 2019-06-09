using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class NameCodeUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Schools",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Programmes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");
        }
    }
}

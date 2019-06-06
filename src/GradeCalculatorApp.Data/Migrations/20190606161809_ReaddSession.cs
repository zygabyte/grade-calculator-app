using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeCalculatorApp.Data.Migrations
{
    public partial class ReaddSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SessionSemesters");

            migrationBuilder.AddColumn<long>(
                name: "SessionId",
                table: "SessionSemesters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSemesters_SessionId",
                table: "SessionSemesters",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSemesters_Sessions_SessionId",
                table: "SessionSemesters",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSemesters_Sessions_SessionId",
                table: "SessionSemesters");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionSemesters_SessionId",
                table: "SessionSemesters");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SessionSemesters");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SessionSemesters",
                nullable: true);
        }
    }
}

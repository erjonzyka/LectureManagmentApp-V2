using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class FlagAcademicYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Flag",
                table: "VitiAkademik",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                table: "VitiAkademik");
        }
    }
}

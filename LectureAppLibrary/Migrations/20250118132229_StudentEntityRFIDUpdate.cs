using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class StudentEntityRFIDUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RFID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RFID",
                table: "Students");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class GrupiUpdateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmertimiGrupit",
                table: "Grupet");

            migrationBuilder.AddColumn<string>(
                name: "Paraleli",
                table: "Grupet",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Viti",
                table: "Grupet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paraleli",
                table: "Grupet");

            migrationBuilder.DropColumn(
                name: "Viti",
                table: "Grupet");

            migrationBuilder.AddColumn<string>(
                name: "EmertimiGrupit",
                table: "Grupet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

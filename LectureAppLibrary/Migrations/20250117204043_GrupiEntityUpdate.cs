using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class GrupiEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitiAkademik",
                table: "Grupet");

            migrationBuilder.AddColumn<int>(
                name: "VitiAkademikId",
                table: "Grupet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupet_VitiAkademikId",
                table: "Grupet",
                column: "VitiAkademikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupet_VitetAkademike_VitiAkademikId",
                table: "Grupet",
                column: "VitiAkademikId",
                principalTable: "VitetAkademike",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupet_VitetAkademike_VitiAkademikId",
                table: "Grupet");

            migrationBuilder.DropIndex(
                name: "IX_Grupet_VitiAkademikId",
                table: "Grupet");

            migrationBuilder.DropColumn(
                name: "VitiAkademikId",
                table: "Grupet");

            migrationBuilder.AddColumn<string>(
                name: "VitiAkademik",
                table: "Grupet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

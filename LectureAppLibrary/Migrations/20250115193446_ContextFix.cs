using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class ContextFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_VitiAkademik_VitiAkademikId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_VitiAkademik_VakademikID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VitiAkademik",
                table: "VitiAkademik");

            migrationBuilder.RenameTable(
                name: "VitiAkademik",
                newName: "VitetAkademike");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VitetAkademike",
                table: "VitetAkademike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_VitetAkademike_VitiAkademikId",
                table: "Schedules",
                column: "VitiAkademikId",
                principalTable: "VitetAkademike",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_VitetAkademike_VakademikID",
                table: "Students",
                column: "VakademikID",
                principalTable: "VitetAkademike",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_VitetAkademike_VitiAkademikId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_VitetAkademike_VakademikID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VitetAkademike",
                table: "VitetAkademike");

            migrationBuilder.RenameTable(
                name: "VitetAkademike",
                newName: "VitiAkademik");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VitiAkademik",
                table: "VitiAkademik",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_VitiAkademik_VitiAkademikId",
                table: "Schedules",
                column: "VitiAkademikId",
                principalTable: "VitiAkademik",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_VitiAkademik_VakademikID",
                table: "Students",
                column: "VakademikID",
                principalTable: "VitiAkademik",
                principalColumn: "Id");
        }
    }
}

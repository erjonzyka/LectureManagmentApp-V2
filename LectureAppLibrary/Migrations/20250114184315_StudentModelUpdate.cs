using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class StudentModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VakademikID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_VakademikID",
                table: "Students",
                column: "VakademikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_VitiAkademik_VakademikID",
                table: "Students",
                column: "VakademikID",
                principalTable: "VitiAkademik",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_VitiAkademik_VakademikID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_VakademikID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VakademikID",
                table: "Students");
        }
    }
}

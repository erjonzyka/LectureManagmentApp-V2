using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class Studgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grupet_GrupiID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "GrupiID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grupet_GrupiID",
                table: "Students",
                column: "GrupiID",
                principalTable: "Grupet",
                principalColumn: "GrupiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grupet_GrupiID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "GrupiID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grupet_GrupiID",
                table: "Students",
                column: "GrupiID",
                principalTable: "Grupet",
                principalColumn: "GrupiID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

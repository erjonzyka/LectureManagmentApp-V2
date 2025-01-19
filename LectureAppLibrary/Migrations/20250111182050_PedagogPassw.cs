using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class PedagogPassw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedagoget_Departmentet_DepartmentID",
                table: "Pedagoget");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Pedagoget",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Pedagoget",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedagoget_Departmentet_DepartmentID",
                table: "Pedagoget",
                column: "DepartmentID",
                principalTable: "Departmentet",
                principalColumn: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedagoget_Departmentet_DepartmentID",
                table: "Pedagoget");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Pedagoget");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Pedagoget",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedagoget_Departmentet_DepartmentID",
                table: "Pedagoget",
                column: "DepartmentID",
                principalTable: "Departmentet",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

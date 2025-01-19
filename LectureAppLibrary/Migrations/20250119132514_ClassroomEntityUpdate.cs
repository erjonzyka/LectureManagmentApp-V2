using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class ClassroomEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Salla",
                table: "Classrooms");

            migrationBuilder.AlterColumn<int>(
                name: "PedagogLendaID",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SallaNo",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules",
                column: "PedagogLendaID",
                principalTable: "PedagogLenda",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SallaNo",
                table: "Classrooms");

            migrationBuilder.AlterColumn<int>(
                name: "PedagogLendaID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salla",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules",
                column: "PedagogLendaID",
                principalTable: "PedagogLenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

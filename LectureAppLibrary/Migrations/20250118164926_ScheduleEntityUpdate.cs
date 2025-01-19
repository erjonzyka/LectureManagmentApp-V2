using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class ScheduleEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lendet_LendaID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Pedagoget_PedagogID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DitaJaves",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "PedagogID",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LendaID",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PedagogLendaID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PedagogLendaID",
                table: "Schedules",
                column: "PedagogLendaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lendet_LendaID",
                table: "Schedules",
                column: "LendaID",
                principalTable: "Lendet",
                principalColumn: "LendaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Pedagoget_PedagogID",
                table: "Schedules",
                column: "PedagogID",
                principalTable: "Pedagoget",
                principalColumn: "PedagogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules",
                column: "PedagogLendaID",
                principalTable: "PedagogLenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lendet_LendaID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Pedagoget_PedagogID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_PedagogLenda_PedagogLendaID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_PedagogLendaID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PedagogLendaID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "PedagogID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LendaID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DitaJaves",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lendet_LendaID",
                table: "Schedules",
                column: "LendaID",
                principalTable: "Lendet",
                principalColumn: "LendaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Pedagoget_PedagogID",
                table: "Schedules",
                column: "PedagogID",
                principalTable: "Pedagoget",
                principalColumn: "PedagogID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

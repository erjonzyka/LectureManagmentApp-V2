using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class AttendanceEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Attendances");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Attendances",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LendaId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OretZhvilluara",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LendaId",
                table: "Attendances",
                column: "LendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lendet_LendaId",
                table: "Attendances",
                column: "LendaId",
                principalTable: "Lendet",
                principalColumn: "LendaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lendet_LendaId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_LendaId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LendaId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "OretZhvilluara",
                table: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

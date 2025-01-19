using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class ScheduleGrupiAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Grupet_GrupiID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_GrupiID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "GrupiID",
                table: "Schedules");

            migrationBuilder.CreateTable(
                name: "ScheduleGrupet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    GrupiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleGrupet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleGrupet_Grupet_GrupiID",
                        column: x => x.GrupiID,
                        principalTable: "Grupet",
                        principalColumn: "GrupiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleGrupet_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGrupet_GrupiID",
                table: "ScheduleGrupet",
                column: "GrupiID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGrupet_ScheduleID",
                table: "ScheduleGrupet",
                column: "ScheduleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleGrupet");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GrupiID",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GrupiID",
                table: "Schedules",
                column: "GrupiID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Grupet_GrupiID",
                table: "Schedules",
                column: "GrupiID",
                principalTable: "Grupet",
                principalColumn: "GrupiID");
        }
    }
}

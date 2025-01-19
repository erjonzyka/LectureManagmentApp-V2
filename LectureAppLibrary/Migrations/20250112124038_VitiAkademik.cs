using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class VitiAkademik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VakademikID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VitiAkademikId",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VitiAkademik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fillimi = table.Column<int>(type: "int", nullable: false),
                    Perfundimi = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitiAkademik", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_VitiAkademikId",
                table: "Schedules",
                column: "VitiAkademikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_VitiAkademik_VitiAkademikId",
                table: "Schedules",
                column: "VitiAkademikId",
                principalTable: "VitiAkademik",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_VitiAkademik_VitiAkademikId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "VitiAkademik");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_VitiAkademikId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "VakademikID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "VitiAkademikId",
                table: "Schedules");
        }
    }
}

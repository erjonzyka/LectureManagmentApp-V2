using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class SecretaryPsw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Sekretaret",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Sekretaret");
        }
    }
}

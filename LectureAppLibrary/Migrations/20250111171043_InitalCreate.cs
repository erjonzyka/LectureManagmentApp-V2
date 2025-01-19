using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassroomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapaciteti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassroomID);
                });

            migrationBuilder.CreateTable(
                name: "Departmentet",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmentet", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Grupet",
                columns: table => new
                {
                    GrupiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmertimiGrupit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitiAkademik = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupet", x => x.GrupiID);
                });

            migrationBuilder.CreateTable(
                name: "Sekretaret",
                columns: table => new
                {
                    SecretaryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sekretaret", x => x.SecretaryID);
                });

            migrationBuilder.CreateTable(
                name: "Lendet",
                columns: table => new
                {
                    LendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriLendes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kredite = table.Column<int>(type: "int", nullable: false),
                    OreLeksioni = table.Column<int>(type: "int", nullable: false),
                    OreSeminari = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendet", x => x.LendaID);
                    table.ForeignKey(
                        name: "FK_Lendet_Departmentet_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departmentet",
                        principalColumn: "DepartmentID");
                });

            migrationBuilder.CreateTable(
                name: "Pedagoget",
                columns: table => new
                {
                    PedagogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedagoget", x => x.PedagogID);
                    table.ForeignKey(
                        name: "FK_Pedagoget_Departmentet_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departmentet",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KartaStatus = table.Column<bool>(type: "bit", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrupiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Grupet_GrupiID",
                        column: x => x.GrupiID,
                        principalTable: "Grupet",
                        principalColumn: "GrupiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedagogLenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedagogID = table.Column<int>(type: "int", nullable: false),
                    LendaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedagogLenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedagogLenda_Lendet_LendaID",
                        column: x => x.LendaID,
                        principalTable: "Lendet",
                        principalColumn: "LendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedagogLenda_Pedagoget_PedagogID",
                        column: x => x.PedagogID,
                        principalTable: "Pedagoget",
                        principalColumn: "PedagogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DitaJaves = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LendaID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    PedagogID = table.Column<int>(type: "int", nullable: false),
                    ClassroomID = table.Column<int>(type: "int", nullable: false),
                    GrupiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedules_Classrooms_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Grupet_GrupiID",
                        column: x => x.GrupiID,
                        principalTable: "Grupet",
                        principalColumn: "GrupiID");
                    table.ForeignKey(
                        name: "FK_Schedules_Lendet_LendaID",
                        column: x => x.LendaID,
                        principalTable: "Lendet",
                        principalColumn: "LendaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Pedagoget_PedagogID",
                        column: x => x.PedagogID,
                        principalTable: "Pedagoget",
                        principalColumn: "PedagogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendances_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ScheduleID",
                table: "Attendances",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentID",
                table: "Attendances",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lendet_DepartmentID",
                table: "Lendet",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedagoget_DepartmentID",
                table: "Pedagoget",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PedagogLenda_LendaID",
                table: "PedagogLenda",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_PedagogLenda_PedagogID",
                table: "PedagogLenda",
                column: "PedagogID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassroomID",
                table: "Schedules",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GrupiID",
                table: "Schedules",
                column: "GrupiID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LendaID",
                table: "Schedules",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PedagogID",
                table: "Schedules",
                column: "PedagogID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GrupiID",
                table: "Students",
                column: "GrupiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "PedagogLenda");

            migrationBuilder.DropTable(
                name: "Sekretaret");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Lendet");

            migrationBuilder.DropTable(
                name: "Pedagoget");

            migrationBuilder.DropTable(
                name: "Grupet");

            migrationBuilder.DropTable(
                name: "Departmentet");
        }
    }
}

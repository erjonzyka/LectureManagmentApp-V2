﻿// <auto-generated />
using System;
using LectureAppLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LectureAppLibrary.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20250121173904_FailedStudentsEntityAdd")]
    partial class FailedStudentsEntityAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LectureAppLibrary.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceID"), 1L, 1);

                    b.Property<int?>("LendaId")
                        .HasColumnType("int");

                    b.Property<int?>("OretZhvilluara")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleID")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("AttendanceID");

                    b.HasIndex("LendaId");

                    b.HasIndex("ScheduleID");

                    b.HasIndex("StudentID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Classroom", b =>
                {
                    b.Property<int>("ClassroomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassroomID"), 1L, 1);

                    b.Property<int>("Kapaciteti")
                        .HasColumnType("int");

                    b.Property<int>("SallaNo")
                        .HasColumnType("int");

                    b.HasKey("ClassroomID");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departmentet");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.FailedStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LendaId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LendaId");

                    b.HasIndex("StudentId");

                    b.ToTable("FailedStudents");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Grupi", b =>
                {
                    b.Property<int>("GrupiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GrupiID"), 1L, 1);

                    b.Property<string>("Paraleli")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Viti")
                        .HasColumnType("int");

                    b.Property<int?>("VitiAkademikId")
                        .HasColumnType("int");

                    b.HasKey("GrupiID");

                    b.HasIndex("VitiAkademikId");

                    b.ToTable("Grupet");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Lenda", b =>
                {
                    b.Property<int>("LendaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LendaID"), 1L, 1);

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("EmriLendes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kredite")
                        .HasColumnType("int");

                    b.Property<int>("OreLeksioni")
                        .HasColumnType("int");

                    b.Property<int>("OreSeminari")
                        .HasColumnType("int");

                    b.HasKey("LendaID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Lendet");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Pedagog", b =>
                {
                    b.Property<int>("PedagogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedagogID"), 1L, 1);

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PedagogID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Pedagoget");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.PedagogLenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LendaID")
                        .HasColumnType("int");

                    b.Property<int>("PedagogID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LendaID");

                    b.HasIndex("PedagogID");

                    b.ToTable("PedagogLenda");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleID"), 1L, 1);

                    b.Property<int>("ClassroomID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LendaID")
                        .HasColumnType("int");

                    b.Property<int?>("PedagogID")
                        .HasColumnType("int");

                    b.Property<int?>("PedagogLendaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VakademikID")
                        .HasColumnType("int");

                    b.Property<int?>("VitiAkademikId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleID");

                    b.HasIndex("ClassroomID");

                    b.HasIndex("LendaID");

                    b.HasIndex("PedagogID");

                    b.HasIndex("PedagogLendaID");

                    b.HasIndex("VitiAkademikId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.ScheduleGrupi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GrupiID")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrupiID");

                    b.HasIndex("ScheduleID");

                    b.ToTable("ScheduleGrupet");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Sekretaria", b =>
                {
                    b.Property<int>("SecretaryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SecretaryID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SecretaryID");

                    b.ToTable("Sekretaret");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("GrupiID")
                        .HasColumnType("int");

                    b.Property<bool>("KartaStatus")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RFID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VakademikID")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.HasIndex("GrupiID");

                    b.HasIndex("VakademikID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.VitiAkademik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Fillimi")
                        .HasColumnType("int");

                    b.Property<int?>("Flag")
                        .HasColumnType("int");

                    b.Property<int>("Perfundimi")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VitetAkademike");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Attendance", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Lenda", "Lenda")
                        .WithMany()
                        .HasForeignKey("LendaId");

                    b.HasOne("LectureAppLibrary.Models.Schedule", "Schedule")
                        .WithMany("Attendances")
                        .HasForeignKey("ScheduleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureAppLibrary.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lenda");

                    b.Navigation("Schedule");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.FailedStudent", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Lenda", "Lenda")
                        .WithMany()
                        .HasForeignKey("LendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureAppLibrary.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lenda");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Grupi", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.VitiAkademik", "VitiAkademik")
                        .WithMany()
                        .HasForeignKey("VitiAkademikId");

                    b.Navigation("VitiAkademik");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Lenda", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Department", "Department")
                        .WithMany("Lendet")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Pedagog", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Department", "Department")
                        .WithMany("Pedagoget")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.PedagogLenda", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Lenda", "Lenda")
                        .WithMany("PedagogLenda")
                        .HasForeignKey("LendaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureAppLibrary.Models.Pedagog", "Pedagog")
                        .WithMany("PedagogLenda")
                        .HasForeignKey("PedagogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lenda");

                    b.Navigation("Pedagog");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Schedule", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Classroom", "Classroom")
                        .WithMany("Schedules")
                        .HasForeignKey("ClassroomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureAppLibrary.Models.Lenda", null)
                        .WithMany("Schedules")
                        .HasForeignKey("LendaID");

                    b.HasOne("LectureAppLibrary.Models.Pedagog", null)
                        .WithMany("Schedules")
                        .HasForeignKey("PedagogID");

                    b.HasOne("LectureAppLibrary.Models.PedagogLenda", "PedagogLenda")
                        .WithMany("Schedules")
                        .HasForeignKey("PedagogLendaID");

                    b.HasOne("LectureAppLibrary.Models.VitiAkademik", "VitiAkademik")
                        .WithMany()
                        .HasForeignKey("VitiAkademikId");

                    b.Navigation("Classroom");

                    b.Navigation("PedagogLenda");

                    b.Navigation("VitiAkademik");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.ScheduleGrupi", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Grupi", "Grupi")
                        .WithMany("ScheduleGrupet")
                        .HasForeignKey("GrupiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LectureAppLibrary.Models.Schedule", "Schedule")
                        .WithMany("ScheduleGrupet")
                        .HasForeignKey("ScheduleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grupi");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Student", b =>
                {
                    b.HasOne("LectureAppLibrary.Models.Grupi", "Grupi")
                        .WithMany("Students")
                        .HasForeignKey("GrupiID");

                    b.HasOne("LectureAppLibrary.Models.VitiAkademik", "VitiAkademik")
                        .WithMany()
                        .HasForeignKey("VakademikID");

                    b.Navigation("Grupi");

                    b.Navigation("VitiAkademik");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Classroom", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Department", b =>
                {
                    b.Navigation("Lendet");

                    b.Navigation("Pedagoget");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Grupi", b =>
                {
                    b.Navigation("ScheduleGrupet");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Lenda", b =>
                {
                    b.Navigation("PedagogLenda");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Pedagog", b =>
                {
                    b.Navigation("PedagogLenda");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.PedagogLenda", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Schedule", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("ScheduleGrupet");
                });

            modelBuilder.Entity("LectureAppLibrary.Models.Student", b =>
                {
                    b.Navigation("Attendances");
                });
#pragma warning restore 612, 618
        }
    }
}

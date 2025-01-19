using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LectureAppLibrary
{
    public class MyContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Department> Departmentet { get; set; }
        public DbSet<Grupi> Grupet { get; set; }
        public DbSet<Lenda> Lendet { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Sekretaria> Sekretaret { get; set; }
        public DbSet<Pedagog> Pedagoget { get; set; }
        public DbSet<PedagogLenda> PedagogLenda { get; set; }
        public DbSet<ScheduleGrupi> ScheduleGrupet { get; set; }
        public DbSet<VitiAkademik> VitetAkademike { get; set; }


    }
}

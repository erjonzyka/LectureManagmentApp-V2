using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class ProductViewModel
    {
        public Department? Department { get; set; } = new Department();
        public List<Department>? Departments { get; set; }
        public Pedagog? Pedagog { get; set; }
        public Lenda? Lenda { get; set; }
        public List<Lenda>? Lendet { get; set; }
        public List<VitiAkademik>? VitetAkademike { get; set; }
        public VitiAkademik? VitiAkademik { get; set; }
        public List<Grupi>? TeGjithaGrupet { get; set; }
        public Grupi? Grupi { get; set; }
        public List<Student>? Students { get; set;}
        public List<Pedagog>? Pedagoget { get; set; }
        public Classroom? Classroom { get; set; }
        public List<Classroom>? Classrooms { get; set; }
        public List<PedagogLenda>? PedagogetDheLendet { get; set; }
        public Schedule? Schedule { get; set; }
        public List<ScheduleGrupi>? ListScheduleGrupi {  get; set; }
        public Attendance? Attendance { get; set; }
       // public Dictionary<Grupi, List<Student>> StudentDictionary { get; set; } 
        public List<Attendance>? Attendances { get; set; }
    }
}

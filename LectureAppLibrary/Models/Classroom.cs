using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Classroom
    {
        [Key]
        public int ClassroomID { get; set; }
        public string Salla { get; set; }
        public int Kapaciteti{ get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } 

        public int ScheduleID { get; set; }
        public int StudentID { get; set; }

 
        public Schedule? Schedule { get; set; }
        public Student? Student { get; set; }
    }
}

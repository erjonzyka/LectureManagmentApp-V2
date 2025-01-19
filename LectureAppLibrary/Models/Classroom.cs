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
        [Required(ErrorMessage ="Vendosni numrin e salles")]
        public int SallaNo { get; set; }
        public int Kapaciteti{ get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}

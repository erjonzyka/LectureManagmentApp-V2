using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class ScheduleGrupi
    {
        [Key]
        public int Id { get; set; }
        public int ScheduleID { get; set; }
        public Schedule? Schedule { get; set; }

        public int GrupiID { get; set; }
        public Grupi? Grupi { get; set; }
    }

}

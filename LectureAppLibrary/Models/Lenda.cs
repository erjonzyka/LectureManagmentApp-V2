using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Lenda
    {
        [Key]
        public int LendaID { get; set; }
        public string EmriLendes { get; set; }
        public int Kredite { get; set; }
        public int OreLeksioni { get; set; }
        public int OreSeminari { get; set; }

    
        public int? DepartmentID { get; set; }

        
        public Department? Department { get; set; }
        public ICollection<PedagogLenda>? PedagogLenda { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}

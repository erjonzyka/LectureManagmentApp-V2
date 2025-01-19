using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LectureAppLibrary.Models
{
    public class PedagogLenda
    {
        [Key]
        public int Id { get; set; }
        public int PedagogID { get; set; }
        public int LendaID { get; set; }

        public Pedagog? Pedagog { get; set; }
        public Lenda? Lenda { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}

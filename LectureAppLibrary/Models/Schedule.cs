using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LectureAppLibrary.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int ClassroomID { get; set; }
        public string? Status { get; set; } = "Krijuar";

        [Required]
        public int VakademikID { get; set; }

        public int? PedagogLendaID { get; set; } 
        public PedagogLenda? PedagogLenda { get; set; } 

        public Classroom? Classroom { get; set; }
        public VitiAkademik? VitiAkademik { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<ScheduleGrupi>? ScheduleGrupet { get; set; }
    }
}

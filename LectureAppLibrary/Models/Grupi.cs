using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Grupi
    {
        [Key]
        public int GrupiID { get; set; }
        [Required(ErrorMessage = "Emërtimi i grupit është i detyrueshëm.")]
        [StringLength(1, ErrorMessage = "Paraleli mund të jetë vetëm një karakter.")]
        [RegularExpression(@"^[A-Za-z]$", ErrorMessage = "Paraleli duhet të jetë një shkronjë e vetme.")]
        public string Paraleli { get; set; }
        public int Viti { get; set; } = 1;
        public int? VitiAkademikId { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<ScheduleGrupi>? ScheduleGrupet { get; set; }
        public VitiAkademik? VitiAkademik { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class VitiAkademik
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Fillimi { get; set; }
        [Required]
        public int Perfundimi { get; set; }
        [Required]
        public string Status { get; set; } = "Rregjistrime";
        public int? Flag { get; set; } = 1;
    }
}

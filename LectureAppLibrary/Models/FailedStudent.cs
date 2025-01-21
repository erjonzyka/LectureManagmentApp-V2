using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class FailedStudent
    {
        [Key]
        public int Id {  get; set; }
        public int StudentId { get; set; }
        public int LendaId {  get; set; }
        public Student? Student {  get; set; }
        public Lenda? Lenda {  get; set; }
    }
}

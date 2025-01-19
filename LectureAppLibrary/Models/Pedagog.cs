using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Pedagog
    {
        [Key]
        public int PedagogID { get; set; }

        [Required(ErrorMessage = "Fusha e emrit duhet te plotesohet")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fusha e mbiemrit duhet te plotesohet")]
        [StringLength(100)]
        public string LastName { get; set; }
        //[UniqueEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ju lutem vendosni nje password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords duhet te jene te njejta!")]
        public string Pwconfirm { get; set; }
        public int? DepartmentID { get; set; }

        public Department? Department { get; set; }
        public ICollection<PedagogLenda>? PedagogLenda { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}

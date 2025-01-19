using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required(ErrorMessage = "Fusha e emrit duhet te plotesohet")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fusha e mbiemrit duhet te plotesohet")]
        [StringLength(100)]
        public string LastName { get; set; }
        [UniqueEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ju lutem vendosni nje password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

    }
}

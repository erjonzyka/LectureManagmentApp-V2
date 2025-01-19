using LectureAppLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email is required")]
        [MinLength(3, ErrorMessage = "Email must be at least 3 characters")]
        public string LEmail { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string LPassword { get; set; }

    }
}




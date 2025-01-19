using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LectureAppLibrary;

namespace LectureAppLibrary.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required(ErrorMessage ="Fusha e emrit duhet te plotesohet")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fusha e mbiemrit duhet te plotesohet")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        //[UniqueEmail]
        public string Email { get; set; }

        [Required]
        public bool KartaStatus { get; set; } = true;

        public string? RFID { get; set; }

        [Required(ErrorMessage ="Ju lutem vendosni nje password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Passwords duhet te jene te njejta!")]
        public string Pwconfirm { get; set; }

        [ForeignKey("Group")]
        public int? GrupiID { get; set; }
        [ForeignKey("VitiAkademik")]
        public int? VakademikID { get; set; }

       
        public Grupi? Grupi { get; set; }
        public VitiAkademik? VitiAkademik { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}



public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        if (value == null)
        {

            return new ValidationResult("Email eshte i detyrueshem!");
        }


        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if (_context.Students.Any(e => e.Email == value.ToString()) || _context.Sekretaret.Any(e => e.Email == value.ToString())
            || _context.Pedagoget.Any(e => e.Email == value.ToString()) || _context.Admins.Any(e => e.Email == value.ToString()))
        {

            return new ValidationResult("Adresa ekziston ne sistem!");
        }
        else
        {

            return ValidationResult.Success;
        }
    }
}
using LectureAppLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LectureAppLibrary.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Vendos emrin e departamentit")]
        [UniqueDept]
        public string DepartmentName { get; set; }

        public ICollection<Pedagog>? Pedagoget { get; set; }
        public ICollection<Lenda>? Lendet { get; set; }
    }
}


public class UniqueDeptAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        if (value == null)
        {

            return new ValidationResult("Emri i departamentit duhet te vendoset!");
        }


        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if (_context.Departmentet.Any(e => e.DepartmentName == value.ToString()))
        {

            return new ValidationResult("Departamenti eshte shtuar ne sistem!");
        }
        else
        {

            return ValidationResult.Success;
        }
    }
}
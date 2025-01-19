using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Attributes
{
    public class ExistingEmailAdminAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {

                return new ValidationResult("Email is required!");
            }


            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

            if (!_context.Admins.Any(e => e.Email == value.ToString()))
            {

                return new ValidationResult("User not registered");
            }
            else
            {

                return ValidationResult.Success;
            }
        }
    }

}

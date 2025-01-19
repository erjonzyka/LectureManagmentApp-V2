using LectureAppLibrary.Interfaces;
using LectureAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Services
{
    public class LoginService : ILoginService
    {
        private MyContext _context;

        public LoginService(MyContext context)
        {
            _context = context;
        }

        public bool EmailExists(string email)
        {
            return _context.Students.Any(e => e.Email == email) ||
                       _context.Sekretaret.Any(e => e.Email == email) ||
            _context.Pedagoget.Any(e => e.Email == email) ||
                       _context.Admins.Any(e => e.Email == email) || _context.Sekretaret.Any(e=> e.Email == email);
        }

        public bool KontrolloVitAkademik()
        {
            return _context.VitetAkademike.Any(e => e.Flag == 1); ;
        }


        public string GenerateRFID(Student student)
        {
            string initials = $"{student.FirstName[0]}{student.LastName[0]}".ToUpper();

            
            string rfid = $"{initials}{student.StudentID + 1000}";

            return rfid;
        }

    }
}

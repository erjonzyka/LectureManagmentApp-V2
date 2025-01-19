using LectureAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Interfaces
{
    public interface ILoginService
    {
        bool EmailExists(string email);
        bool KontrolloVitAkademik();
        string GenerateRFID(Student st);
    }
}

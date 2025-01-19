using Microsoft.AspNetCore.Mvc;

namespace LectureManagmentApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

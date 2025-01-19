using Microsoft.AspNetCore.Mvc;

namespace LectureManagmentApp.Controllers
{
    public class PedagogController : Controller
    {
        [PedagogCheck]
        public IActionResult Index()
        {
            return View();
        }
    }
}

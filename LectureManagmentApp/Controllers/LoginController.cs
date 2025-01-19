using LectureAppLibrary;
using LectureAppLibrary.Interfaces;
using LectureAppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LectureManagmentApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private MyContext _context;
        public ILoginService _login { get; }
        public IAdminService _admin { get; }

        public LoginController(ILogger<LoginController> logger, MyContext context, ILoginService login, IAdminService admin)
        {
            _login = login;
            _logger = logger;
            _context = context;
            _admin = admin;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("admin/login")]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost("admin/login")]
        public IActionResult AdminLogin(UserLogin admini)
        {
            if (ModelState.IsValid)
            {
                Admin? CurrentAdmin = _context.Admins.FirstOrDefault(e => e.Email == admini.LEmail);
                if (CurrentAdmin == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("AdminLogin");
                }
                PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                var result = hasher.VerifyHashedPassword(admini, CurrentAdmin.Password, admini.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("AdminLogin");
                }
                HttpContext.Session.SetInt32("UserId", CurrentAdmin.AdminID);
                HttpContext.Session.SetInt32("AdminId", CurrentAdmin.AdminID);
                HttpContext.Session.SetString("Roli", "Admin");

                if (_login.KontrolloVitAkademik())
                {
                    HttpContext.Session.SetInt32("VAkademikId", _admin.MerrVitinAktual().Id);
                    HttpContext.Session.SetString("VAkademikStatus", _admin.MerrVitinAktual().Status);

                }

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("AdminLogin");
            }
        }

        [AdminCheck]
        [HttpGet("professor/reg")]
        public IActionResult ProfessorReg()
        {
            return View();
        }

        [AdminCheck]
        [HttpPost("professor/reg")]
        public IActionResult ProfessorReg(Pedagog user)
        {
            if (_login.EmailExists(user.Email))
            {
                ModelState.AddModelError("Email", "Adresa ekziston në sistem!");
                return View("ProfessorReg", user);
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<Pedagog> Hasher = new PasswordHasher<Pedagog>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("ProfessorReg");
            }
        }

        [HttpGet("pedagog/login")]
        public IActionResult PedagogLogin()
        {
            return View();
        }

        [HttpPost("professor/login")]
        public IActionResult ProfessorLogin(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                Pedagog? CurrentUser = _context.Pedagoget.FirstOrDefault(e => e.Email == user.LEmail);
                if (CurrentUser == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("PedagogLogin");
                }
                PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                var result = hasher.VerifyHashedPassword(user, CurrentUser.Password, user.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("PedagogLogin");
                }
                HttpContext.Session.SetInt32("UserId", CurrentUser.PedagogID);
                HttpContext.Session.SetInt32("PedagogId", CurrentUser.PedagogID);
                HttpContext.Session.SetString("Roli", "Pedagog");
                if (_login.KontrolloVitAkademik())
                {
                    HttpContext.Session.SetInt32("VAkademikId", _admin.MerrVitinAktual().Id);
                    HttpContext.Session.SetString("VAkademikStatus", _admin.MerrVitinAktual().Status);

                }

                return RedirectToAction("Index", "Pedagog");
            }
            else
            {
                return View("PedagogLogin");
            }
        }

        [AdminCheck]
        [HttpGet("secretary/reg")]
        public IActionResult SecretaryReg()
        {
            return View();
        }

        [AdminCheck]
        [HttpPost("secretary/add")]
        public IActionResult SecretaryAdd(Sekretaria user)
        {
            if (_login.EmailExists(user.Email))
            {
                ModelState.AddModelError("Email", "Adresa ekziston në sistem!");
                return View("SecretaryReg", user);
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<Sekretaria> Hasher = new PasswordHasher<Sekretaria>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("SecretaryReg");
            }
        }


        [HttpGet("secretary/login")]
        public IActionResult SecretaryLogin()
        {
            return View();
        }


        [HttpPost("secretary/login")]
        public IActionResult SecretaryLogin(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                Sekretaria? CurrentUser = _context.Sekretaret.FirstOrDefault(e => e.Email == user.LEmail);
                if (CurrentUser == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("PedagogLogin");
                }
                PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                var result = hasher.VerifyHashedPassword(user, CurrentUser.Password, user.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("PedagogLogin");
                }
                HttpContext.Session.SetInt32("UserId", CurrentUser.SecretaryID);
                HttpContext.Session.SetInt32("SecretaryId", CurrentUser.SecretaryID);
                HttpContext.Session.SetString("Roli", "Sekretar");
                if (_login.KontrolloVitAkademik())
                {
                    HttpContext.Session.SetInt32("VAkademikId", _admin.MerrVitinAktual().Id);
                    HttpContext.Session.SetString("VAkademikStatus", _admin.MerrVitinAktual().Status);

                }
                return RedirectToAction("Index", "Sekretaria");
            }
            else
            {
                return View("SecretaryLogin");
            }
        }


        [HttpGet("student/reg")]
        public IActionResult StudentReg()
        {

            if(HttpContext.Session.GetInt32("AdminId") == null && HttpContext.Session.GetInt32("SecretaryId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("student/add")]
        public IActionResult StudentAdd(Student user)
        {
            if (_login.EmailExists(user.Email))
            {
                ModelState.AddModelError("Email", "Adresa ekziston në sistem!");
                return View("StudentReg", user);
            }
            if (HttpContext.Session.GetInt32("AdminId") == null && HttpContext.Session.GetInt32("SecretaryId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                PasswordHasher<Student> Hasher = new PasswordHasher<Student>();
                user.Password = Hasher.HashPassword(user, user.Password);
                user.VakademikID = (int)HttpContext.Session.GetInt32("VAkademikId");
                _context.Add(user);
                _context.SaveChanges();
                user.RFID = _login.GenerateRFID(user);
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("StudentReg");
            }
        }

        [HttpGet("student/login")]
        public IActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost("student/login")]
        public IActionResult StudentLogin(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                Student? CurrentUser = _context.Students.FirstOrDefault(e => e.Email == user.LEmail);
                if (CurrentUser == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("StudentLogin");
                }
                PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                var result = hasher.VerifyHashedPassword(user, CurrentUser.Password, user.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("PedagogLogin");
                }
                HttpContext.Session.SetInt32("UserId", CurrentUser.StudentID);
                HttpContext.Session.SetInt32("StudentId", CurrentUser.StudentID);
                HttpContext.Session.SetString("Roli", "Student");
                if (_login.KontrolloVitAkademik())
                {
                    HttpContext.Session.SetInt32("VAkademikId", _admin.MerrVitinAktual().Id);
                    HttpContext.Session.SetString("VAkademikStatus", _admin.MerrVitinAktual().Status);

                }

                return RedirectToAction("Index", "Student");
            }
            else
            {
                return View("SecretaryLogin");
            }
        }



        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}

public class AdminCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("AdminId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}

public class PedagogCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("PedagogId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}

public class SecretaryCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("SecretaryId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
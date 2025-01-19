using LectureAppLibrary.Interfaces;
using LectureAppLibrary;
using LectureAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagmentApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;

        public IAdminService _admin { get; }
        public IWebHostEnvironment _environment { get; }
        public MyContext _context { get; }

        public AdminController(ILogger<AdminController> logger, IAdminService admin, IWebHostEnvironment environment, MyContext context)
        {
            _logger = logger;
            _admin = admin;
            _environment = environment;
            _context = context;
        }


        [AdminCheck]
        public IActionResult Index()
        {
            return View();
        }

        [AdminCheck]
        [HttpGet("krijo/departament")]
        public IActionResult KrijoDepartament()
        {
            ProductViewModel pwm = new ProductViewModel
            {
                Department = new Department(), 
                Departments = _admin.TeGjithaDepartamentet()
            };
            return View(pwm);
        }

        [AdminCheck]
        [HttpPost]
        public IActionResult ShtoDepartament(ProductViewModel d)
        {
            if (ModelState.IsValid)
            {
                _admin.ShtoDepartament(d.Department);
                return RedirectToAction("KrijoDepartament");
            }
           // ProductViewModel pwm = new ProductViewModel();
            d.Departments = _admin.TeGjithaDepartamentet();
            //pwm.Department = d;
            return View("KrijoDepartament", d);
        }

        [AdminCheck]
        [HttpGet("fshi/dep/{id}")]
        public IActionResult FshiDepartament(int id)
        {
            if (!_admin.FshiDepartament(id))
            {
                return RedirectToAction("KrijoDepartament", new { message = "Fshirja e departamentit deshtoi!" });
            }

            return RedirectToAction("KrijoDepartament", new { message = "Departamenti u fshi me sukses!" });
        }

        [AdminCheck]
        [HttpGet("shfaq/pedagog")]
        public IActionResult ShfaqPedagoget()
        {
            var pedagogList = _admin.ShfaqPedagoget(); 
            return View(pedagogList);
        }

        [AdminCheck]
        [HttpGet("ndrysho/pedagog/{id}")]
        public IActionResult NdryshoPedagog (int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Pedagog = _admin.GjejPedagog(id);
            pvm.Departments = _admin.TeGjithaDepartamentet();
            return View(pvm);
        }

        [AdminCheck]
        [HttpPost("ruaj/ndryshimet/pedagog/{id}")]
        public IActionResult RuajNdryshimetPedagog(ProductViewModel pvm, int id)
        {
            if (string.IsNullOrWhiteSpace(pvm.Pedagog.FirstName))
            {
                ModelState.AddModelError("Pedagog.FirstName", "Emri është i detyrueshëm.");
                pvm.Departments = _admin.TeGjithaDepartamentet();
                return View("NdryshoPedagog", pvm);
            }

            if (string.IsNullOrWhiteSpace(pvm.Pedagog.LastName))
            {
                ModelState.AddModelError("Pedagog.LastName", "Mbiemri është i detyrueshëm.");
                pvm.Departments = _admin.TeGjithaDepartamentet();
                return View("NdryshoPedagog", pvm);
            }

            if (string.IsNullOrWhiteSpace(pvm.Pedagog.Email))
            {
                ModelState.AddModelError("Pedagog.Email", "Email-i është i detyrueshëm.");
                pvm.Departments = _admin.TeGjithaDepartamentet();
                return View("NdryshoPedagog", pvm);
            }
            else { 
                _admin.RuajNdryshimetPedagog(pvm.Pedagog, id);
                return RedirectToAction("ShfaqPedagoget");
            }
        }

        [AdminCheck]
        [HttpGet("shto/lende/form")]
        public IActionResult ShtoLendeForm()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Lendet = _admin.TeGjithaLendet();
            pvm.Departments = _admin.TeGjithaDepartamentet();
            return View(pvm);
        }

        [AdminCheck]
        [HttpPost("shto/lende")]
        public IActionResult ShtoLende(ProductViewModel pvm)
        {
            if (_admin.KontrolloLende(pvm.Lenda.EmriLendes))
            {
                ModelState.AddModelError("Lenda.EmriLendes", "Kjo Lende ekziston ne sistem.");
                pvm.Lendet = _admin.TeGjithaLendet();
                pvm.Departments = _admin.TeGjithaDepartamentet();
                return View("ShtoLendeForm", pvm);
            }
            if (!_admin.RuajLende(pvm.Lenda))
            {
                return RedirectToAction("ShtoLendeForm", new { message = "Ruajtja e lendes deshtoi!" });
            }

            return RedirectToAction("ShtoLendeForm", new { message = "Lenda u ruajt me sukses!" });

        }

        [AdminCheck]
        [HttpGet("ndrysho/lende/{id}")]
        public IActionResult NdryshoLende(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Lenda = _admin.GjejLenden(id);
            pvm.Departments = _admin.TeGjithaDepartamentet();
            return View(pvm);
        }

        [AdminCheck]
        [HttpPost("ruaj/ndryshim/lenda")]
        public IActionResult RuajNdryshimLenda(ProductViewModel pvm, int id)
        {
            if (_admin.KontrolloLende(pvm.Lenda.EmriLendes, id))
            {
                ModelState.AddModelError("Lenda.EmriLendes", "Kjo Lende ekziston ne sistem.");
                pvm.Lendet = _admin.TeGjithaLendet();
                pvm.Departments = _admin.TeGjithaDepartamentet();
                return View("NdryshoLende", pvm);
            }
            if (!_admin.RuajNdryshimetLenda(pvm.Lenda, id))
            {
                return RedirectToAction("ShtoLendeForm", new { message = "Ruajtja e lendes deshtoi!" });
            }

            return RedirectToAction("ShtoLendeForm", new { message = "Lenda u ruajt me sukses!" });
        }


        [AdminCheck]
        [HttpGet("menaxho/vitin/akademik")]
        public IActionResult MenaxhoVitinAkademikView()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.VitetAkademike = _admin.TeGjithaVitetAkademike();
            pvm.VitiAkademik = _admin.MerrVitinAktual();
            return View(pvm);
        }

        [AdminCheck]
        [HttpGet("hap/vitin/akademik")]
        public IActionResult HapVitinAkademik()
        {
            if (_admin.KontrolloPerVitTeHapurAkademik())
            {
                return RedirectToAction("MenaxhoVitinAkademikView");
            }
            VitiAkademik? vak = new VitiAkademik();
            vak.Fillimi = DateTime.Now.Year;
            vak.Perfundimi = vak.Fillimi + 1;
            _context.Add(vak);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("VAkademikId", vak.Id);
            HttpContext.Session.SetString("VAkademikStatus", vak.Status);
            return RedirectToAction("MenaxhoVitinAkademikView");
        }

        [AdminCheck]
        [HttpPost("ndrysho/stat/viti/akademik/{id}")]
        public IActionResult NdryshoStatusVitAkademik(ProductViewModel pvm, int id)
        {
            VitiAkademik? vak = _admin.MerrVitinAkademik(id);
            vak.Status = pvm.VitiAkademik.Status;
            _context.SaveChanges();
            HttpContext.Session.SetString("VAkademikStatus", vak.Status);
            return RedirectToAction("MenaxhoVitinAkademikView");
        }

        [AdminCheck]
        [HttpGet("mbyll/vitin/akademik/{id}")]
        public IActionResult MbyllVitinAkademik(int id)
        {
            VitiAkademik? vak = _admin.MerrVitinAkademik(id);
            vak.Status = "Mbyllur";
            vak.Flag = 0;
            _context.SaveChanges();
            HttpContext.Session.Remove("VAkademikId");
            HttpContext.Session.Remove("VAkademikStatus");
            return RedirectToAction("MenaxhoVitinAkademikView");
        }

    }
    }


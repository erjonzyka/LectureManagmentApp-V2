using LectureAppLibrary.Interfaces;
using LectureAppLibrary;
using Microsoft.AspNetCore.Mvc;
using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore;


namespace LectureManagmentApp.Controllers
{
    public class SekretariaController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public ISecretaryService _secretary { get; }
        public IAdminService _admin { get; }
        public IWebHostEnvironment _environment { get; }
        public MyContext _context { get; }

        public SekretariaController(ILogger<AdminController> logger, ISecretaryService secretary, IAdminService admin, IWebHostEnvironment environment, MyContext context)
        {
            _logger = logger;
            _secretary = secretary;
            _admin = admin;
            _environment = environment;
            _context = context;
        }







        [SecretaryCheck]
        public IActionResult Index()
        {
            return View();
        }

        [SecretaryCheck]
        [HttpGet("create/group/form")]
        public IActionResult CreateGroupForm()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.TeGjithaGrupet = _secretary.MerrTeGjithaGrupet();
            return View(pvm);
        }


        [SecretaryCheck]
        [HttpPost]
        public IActionResult AddGroup(ProductViewModel pvm)
        {
            pvm.TeGjithaGrupet = _secretary.MerrTeGjithaGrupet();
            pvm.Grupi.VitiAkademikId = (int)HttpContext.Session.GetInt32("VAkademikId");

            if (string.IsNullOrWhiteSpace(pvm.Grupi.Paraleli))
            {
                ModelState.AddModelError("Grupi.Paraleli", "Emri është i detyrueshëm.");
                return View("CreateGroupForm", pvm);
            }

            if (_secretary.CheckExistence(pvm.Grupi.Paraleli, pvm.Grupi.Viti))
            {
                ModelState.AddModelError("Grupi.Paraleli", "Grupi tashme eshte i krijuar ne sistem.");
                return View("CreateGroupForm", pvm);
            }

            _context.Add(pvm.Grupi);
            _context.SaveChanges();
            return RedirectToAction("CreateGroupForm");
        }

        [SecretaryCheck]
        [HttpGet("fshi/group/{id}")]
        public IActionResult FshiGrup(int id)
        {
            if (!_secretary.FshiGrup(id))
            {
                return RedirectToAction("CreateGroupForm", new { message = "Fshirja e grupit deshtoi!" });
            }

            return RedirectToAction("CreateGroupForm", new { message = "Grupi u fshi me sukses!" });
        }

        [SecretaryCheck]
        [HttpGet("grupi/studentet/{id}")]
        public IActionResult StudentetGrupi(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Grupi = _secretary.GetGrupi(id);
            pvm.Grupi.Students = pvm.Grupi.Students.OrderBy(e => e.FirstName).ToList();
            pvm.Students = _secretary.MerrStudentetPaGrup();
            return View(pvm);
        }


        [SecretaryCheck]
        [HttpGet("fshi/student/nga/grupi/{studentid}")]
        public IActionResult HiqNgaGrupi(int studentid)
        {
            Student? studenti = _secretary.GetStudent(studentid);
            int? id = studenti.GrupiID;
            studenti.GrupiID = null;
            _context.SaveChanges();
            return RedirectToAction("StudentetGrupi", new { id = id });
        }


        [SecretaryCheck]
        [HttpPost("shto/ne/grup/{id}")]
        public IActionResult ShtoNeGrup(int id, int StudentID)
        {
            Student? st = _secretary.GetStudent(StudentID);
            st.GrupiID = id;
            _context.SaveChanges();
            return RedirectToAction("StudentetGrupi", new { id = id });
        }

        [SecretaryCheck]
        [HttpGet("pezullo/karten/{id}")]
        public IActionResult PezulloKarteStudenti(int id)
        {
            Student? st = _secretary.GetStudent(id);
            st.KartaStatus = false;
            _context.SaveChanges();
            return RedirectToAction("StudentetGrupi", new { id = st.GrupiID });
        }

        [SecretaryCheck]
        [HttpGet("aktivizo/karten/{id}")]
        public IActionResult AktivizoKarteStudenti(int id)
        {
            Student? st = _secretary.GetStudent(id);
            st.KartaStatus = true;
            _context.SaveChanges();
            return RedirectToAction("StudentetGrupi", new { id = st.GrupiID });
        }

        [SecretaryCheck]
        [HttpGet("pedagoget/lendet")]
        public IActionResult PedagogetDheLendet()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Lendet = _admin.TeGjithaLendet();
            pvm.Pedagoget = _secretary.MerrTeGjithePedagoget();
            return View(pvm);
        }

        [SecretaryCheck]
        [HttpGet("pedagogu/details/{id}")]
        public IActionResult PedagoguDetails(int id)
        {
            ProductViewModel pvm = new ProductViewModel
            {
                Pedagog = _admin.GjejPedagog(id),
                Lendet = _secretary.MerrLendetQeNukJep(id)
            };
            return View(pvm);
        }


        [SecretaryCheck]
        [HttpPost("shto/lende/pedagog/{id}")]
        public IActionResult ShtoLendeNePedagog(int id, int LendaId)
        {
            var pedagog = _context.Pedagoget.FirstOrDefault(p => p.PedagogID == id);
            var lenda = _context.Lendet.FirstOrDefault(l => l.LendaID == LendaId);

            if (pedagog == null || lenda == null)
            {
                TempData["Message"] = "Pedagogu ose lenda nuk u gjet";
                return RedirectToAction("PedagoguDetails", new { id = id });
            }

            var ekzistonLidhja = _secretary.KontrolloLidhjenPedagogLende(id, LendaId);
            if (ekzistonLidhja)
            {
                TempData["Message"] = "Kjo lende tashme eshte e lidhur me kete pedagog.";
                return RedirectToAction("PedagoguDetails", new { id = id });
            }

            var lidhjaRe = new PedagogLenda
            {
                PedagogID = id,
                LendaID = LendaId
            };

            _context.Add(lidhjaRe);
            _context.SaveChanges();

            TempData["Message"] = "Lenda u shtua me sukses!";
            return RedirectToAction("PedagoguDetails", new { id });
        }


        [SecretaryCheck]
        [HttpGet("fshi/lende/pedagog/{id}")]
        public IActionResult FshiLendePedagog(int id)
        {
            var pedagogLenda = _context.PedagogLenda.FirstOrDefault(e => e.Id == id);
            if (pedagogLenda == null)
            {
                TempData["Message"] = "Lidhja nuk u gjet!";
                return RedirectToAction("PedagoguDetails", new { id = id });
            }

            var schedules = _context.Schedules.Where(s => s.PedagogLendaID == id).ToList();

            foreach (var schedule in schedules)
            {
                schedule.PedagogLendaID = null;
            }

            _context.PedagogLenda.Remove(pedagogLenda);
            _context.SaveChanges();

            TempData["Message"] = "Lenda u hoq me sukses!";
            return RedirectToAction("PedagoguDetails", new { id = pedagogLenda.PedagogID });


        }




        [SecretaryCheck]
        [HttpGet("lenda/details/{id}")]
        public IActionResult LendaDetails(int id)
        {
            ProductViewModel pvm = new ProductViewModel
            {
                Lenda = _admin.GjejLenden(id),
                Pedagoget = _secretary.MerrPedagogetQeNukJapin(id)
            };
            return View(pvm);
        }


        [SecretaryCheck]
        [HttpPost("shto/pedagog/lenda/{id}")]
        public IActionResult ShtoPedagogNeLende(int id, int pedagogId)
        {
            var lidhje = new PedagogLenda
            {
                LendaID = id,
                PedagogID = pedagogId
            };

            _context.Add(lidhje);
            _context.SaveChanges();

            TempData["Message"] = "Pedagogu u shtua me sukses!";
            return RedirectToAction("LendaDetails", new { id = id });
        }


        [SecretaryCheck]
        [HttpGet("oret/mesimi/view")]
        public IActionResult OretMesimiView()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.PedagogetDheLendet = _admin.MerrPedagogetDheLendet();
            pvm.Classrooms = _admin.GetAllClassrooms();
            return View(pvm);
        }


        [SecretaryCheck]
        [HttpPost("krijo/rezervim")]
        public IActionResult KrijoRezervim(ProductViewModel pvm)
        {
            pvm.Schedule.VakademikID = (int)HttpContext.Session.GetInt32("VAkademikId");
            List<string> errorMessages = new List<string>();

            if (pvm.Schedule.StartTime == default)
            {
                errorMessages.Add("Data dhe ora e fillimit duhet të plotesohet.");
            }
            if (pvm.Schedule.EndTime == default)
            {
                errorMessages.Add("Data dhe ora e perfundimit duhet të plotesohet.");
            }

            if (pvm.Schedule.StartTime < DateTime.Now)
            {
                errorMessages.Add("Data dhe ora e fillimit nuk mund te jete ne te kaluaren.");
            }
            if (pvm.Schedule.EndTime < DateTime.Now)
            {
                errorMessages.Add("Data dhe ora e perfundimit nuk mund te jete ne te kaluaren.");
            }

            if (pvm.Schedule.StartTime.Hour < 8 || pvm.Schedule.EndTime.Hour > 18)
            {
                errorMessages.Add("Orari duhet te jete ndermjet 08:00 dhe 18:00.");
            }
            if (pvm.Schedule.StartTime >= pvm.Schedule.EndTime)
            {
                errorMessages.Add("Data dhe ora e fillimit duhet te jete me e hershme se ajo e perfundimit.");
            }
            var isClassroomBusy = _context.Schedules.Any(s =>
                s.ClassroomID == pvm.Schedule.ClassroomID &&
                ((pvm.Schedule.StartTime >= s.StartTime && pvm.Schedule.StartTime < s.EndTime) ||
                 (pvm.Schedule.EndTime > s.StartTime && pvm.Schedule.EndTime <= s.EndTime)));

            if (isClassroomBusy)
            {
                errorMessages.Add("Salla eshte e zene per kete orar.");
            }
            var isPedagogLendaBusy = _context.Schedules.Any(s =>
                s.PedagogLendaID == pvm.Schedule.PedagogLendaID &&
                ((pvm.Schedule.StartTime >= s.StartTime && pvm.Schedule.StartTime < s.EndTime) ||
                 (pvm.Schedule.EndTime > s.StartTime && pvm.Schedule.EndTime <= s.EndTime)));

            if (isPedagogLendaBusy)
            {
                errorMessages.Add("Pedagogu/Lenda jane te zena per kete orar.");
            }

            if (errorMessages.Count > 0)
            {
                TempData["Errors"] = errorMessages;
                pvm.PedagogetDheLendet = _admin.MerrPedagogetDheLendet();
                pvm.Classrooms = _admin.GetAllClassrooms();
                return View("OretMesimiView",pvm);
            }

            _context.Schedules.Add(pvm.Schedule);
            _context.SaveChanges();

            TempData["Message"] = "Rezervimi u krijua me sukses.";
            return RedirectToAction("OretMesimiView");

        }

        [SecretaryCheck]
        [HttpGet("orari/view/{id}")]
        public IActionResult OrariView(int id = 1)
        {
            if (id == 1)
            {
                return View(_secretary.GetUpcomingSchedules());
            }
            return View(_secretary.GetPreviousSchedules());
        }


        [SecretaryCheck]
        [HttpGet("schedule/edit/{id}")]
        public IActionResult EditSchedule(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.PedagogetDheLendet = _admin.MerrPedagogetDheLendet();
            pvm.Classrooms = _admin.GetAllClassrooms();
            pvm.Schedule = _secretary.GetSchedule(id);
            return View(pvm);
        }

        [SecretaryCheck]
        [HttpPost("post/edit/schedule/{id}")]
        public IActionResult PostEditSchedule(int id, ProductViewModel pvm)
        {
            pvm.Schedule.VakademikID = (int)HttpContext.Session.GetInt32("VAkademikId");
            List<string> errorMessages = new List<string>();

            if (pvm.Schedule.StartTime == default)
            {
                errorMessages.Add("Data dhe ora e fillimit duhet të plotesohet.");
            }
            if (pvm.Schedule.EndTime == default)
            {
                errorMessages.Add("Data dhe ora e perfundimit duhet të plotesohet.");
            }

            if (pvm.Schedule.StartTime < DateTime.Now)
            {
                errorMessages.Add("Data dhe ora e fillimit nuk mund te jete ne te kaluaren.");
            }
            if (pvm.Schedule.EndTime < DateTime.Now)
            {
                errorMessages.Add("Data dhe ora e perfundimit nuk mund te jete ne te kaluaren.");
            }

            if (pvm.Schedule.StartTime.Hour < 8 || pvm.Schedule.EndTime.Hour > 18)
            {
                errorMessages.Add("Orari duhet te jete ndermjet 08:00 dhe 18:00.");
            }
            if (pvm.Schedule.StartTime >= pvm.Schedule.EndTime)
            {
                errorMessages.Add("Data dhe ora e fillimit duhet te jete me e hershme se ajo e perfundimit.");
            }

            var timeDifference = (pvm.Schedule.EndTime - pvm.Schedule.StartTime).TotalMinutes;
            if (timeDifference < 5)
            {
                errorMessages.Add("Diferenca midis fillimit dhe përfundimit duhet të jetë të paktën 5 minuta.");
            }

            var isClassroomBusy = _context.Schedules.Any(s =>
                s.ClassroomID == pvm.Schedule.ClassroomID &&
                s.ScheduleID != id && // perjashtimi i schedule aktual
                ((pvm.Schedule.StartTime >= s.StartTime && pvm.Schedule.StartTime < s.EndTime) ||
                 (pvm.Schedule.EndTime > s.StartTime && pvm.Schedule.EndTime <= s.EndTime)));

            if (isClassroomBusy)
            {
                errorMessages.Add("Salla eshte e zene per kete orar.");
            }

            var isPedagogLendaBusy = _context.Schedules.Any(s =>
                s.PedagogLendaID == pvm.Schedule.PedagogLendaID &&
                s.ScheduleID != id && // perjashtimi i schedule aktual
                ((pvm.Schedule.StartTime >= s.StartTime && pvm.Schedule.StartTime < s.EndTime) ||
                 (pvm.Schedule.EndTime > s.StartTime && pvm.Schedule.EndTime <= s.EndTime)));

            if (isPedagogLendaBusy)
            {
                errorMessages.Add("Pedagogu/Lenda jane te zena per kete orar.");
            }

            if (errorMessages.Count > 0)
            {
                TempData["Errors"] = errorMessages;
                pvm.PedagogetDheLendet = _admin.MerrPedagogetDheLendet();
                pvm.Classrooms = _admin.GetAllClassrooms();
                return View("EditSchedule", pvm);
            }

            var existingSchedule = _secretary.GetSchedule(id);
            if (existingSchedule == null)
            {
                TempData["Errors"] = new List<string> { "Orari i specifikuar nuk ekziston." };
                return RedirectToAction("OretMesimiView");
            }

            
            existingSchedule.StartTime = pvm.Schedule.StartTime;
            existingSchedule.EndTime = pvm.Schedule.EndTime;
            existingSchedule.ClassroomID = pvm.Schedule.ClassroomID;
            existingSchedule.PedagogLendaID = pvm.Schedule.PedagogLendaID;

            _context.SaveChanges();

            TempData["Message"] = "Orari u ndryshua me sukses.";
            return RedirectToAction("OrariView");
        }


        [SecretaryCheck]
        [HttpGet("schedule/and/groups/{id}")]
        public IActionResult ScheduleAndGroups(int id)
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Schedule = _secretary.GetSchedule(id);
            pvm.ListScheduleGrupi = _secretary.GrupetQeMarrinPjese(id);
            pvm.TeGjithaGrupet = _context.Grupet.Where(g => !_context.ScheduleGrupet
            .Any(sg => sg.GrupiID == g.GrupiID && sg.Schedule.StartTime < pvm.Schedule.EndTime && sg.Schedule.EndTime > pvm.Schedule.StartTime)).ToList();

            return View (pvm);
        }


        [SecretaryCheck]
        [HttpPost("add/groups/to/schedule")]
        public IActionResult AddGroupsToSchedule(int ScheduleID, List<int> selectedGroups)
        {
            var schedule = _context.Schedules
                .Include(s => s.ScheduleGrupet)
                .FirstOrDefault(s => s.ScheduleID == ScheduleID);

            if (schedule == null)
            {
                TempData["Errors"] = new List<string> { "Orari nuk u gjet." };
                return RedirectToAction("OretMesimiView");
            }

            foreach (var groupId in selectedGroups)
            {
                var scheduleGrupi = new ScheduleGrupi
                {
                    ScheduleID = ScheduleID,
                    GrupiID = groupId
                };
                _context.ScheduleGrupet.Add(scheduleGrupi);
            }

            _context.SaveChanges();

            TempData["Message"] = "Grupet u shtuan me sukses në orar.";
            return RedirectToAction("ScheduleAndGroups", new { id = ScheduleID });
        }

        [SecretaryCheck]
        [HttpGet("hiq/grupin/{id}")]
        public IActionResult HiqGrupin(int id)
        {
         
            
            ScheduleGrupi? scheduleGrupi = _context.ScheduleGrupet.Include(e => e.Schedule) .FirstOrDefault(e => e.Id == id);

            if (scheduleGrupi == null)
            {
                TempData["Errors"] = new List<string> { "Grupi nuk u gjet." };
                return RedirectToAction("ScheduleAndGroups", new { id = id });
            }

            if (scheduleGrupi.Schedule.StartTime < DateTime.Now)
            {
                TempData["Errors"] = new List<string> { "Nuk mund të hiqni grupin pasi ora e fillimit të orarit ka kaluar." };
                return RedirectToAction("ScheduleAndGroups", new { id = id });
            }


                _context.ScheduleGrupet.Remove(scheduleGrupi);
                _context.SaveChanges();

                TempData["Message"] = "Grupi u hoq me sukses nga orari.";

            return RedirectToAction("ScheduleAndGroups", new { id = scheduleGrupi.ScheduleID });
        }


        [SecretaryCheck]
        [HttpGet("delete/schedule/{id}")]
        public IActionResult DeleteSchedule(int id)
        {
            var schedule = _context.Schedules.Include(s => s.ScheduleGrupet)
                                             .FirstOrDefault(s => s.ScheduleID == id);

            if (schedule == null)
            {
                TempData["Errors"] = new List<string> { "Rezervimi nuk u gjet." };
                return RedirectToAction("OrariView"); 
            }

            if (schedule.StartTime < DateTime.Now)
            {
                TempData["Errors"] = new List<string> { "Nuk mund të fshini rezervimin pasi ka kaluar koha e fillimit." };
                return RedirectToAction("OrariView");
            }

            var scheduleGrupiList = _context.ScheduleGrupet.Where(sg => sg.ScheduleID == id).ToList();
            _context.ScheduleGrupet.RemoveRange(scheduleGrupiList);

            _context.Schedules.Remove(schedule);
            _context.SaveChanges();

            TempData["Message"] = "Rezervimi u fshi me sukses.";

            return RedirectToAction("OrariView"); 
        }




    }
}

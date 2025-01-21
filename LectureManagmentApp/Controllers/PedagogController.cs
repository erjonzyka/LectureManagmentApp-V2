using LectureAppLibrary.Interfaces;
using LectureAppLibrary;
using Microsoft.AspNetCore.Mvc;
using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LectureManagmentApp.Controllers
{
    public class PedagogController : Controller
    {

        private readonly ILogger<PedagogController> _logger;

        public ISecretaryService _secretary { get; }
        public IPedagogService _pedagog { get; }
        public IWebHostEnvironment _environment { get; }
        public MyContext _context { get; }

        public PedagogController(ILogger<PedagogController> logger, ISecretaryService secretary, IPedagogService pedagog, IWebHostEnvironment environment, MyContext context)
        {
            _logger = logger;
            _secretary = secretary;
            _pedagog = pedagog;
            _environment = environment;
            _context = context;
        }

        [PedagogCheck]
        public IActionResult Index()
        {
            List<Schedule> schedules = _context.Schedules.Include(e => e.PedagogLenda)
                .ThenInclude(e => e.Lenda)
                .Include(e => e.PedagogLenda.Pedagog)
                .Include(e => e.Classroom)
                .Where(e => e.StartTime.Date > DateTime.Today && e.PedagogLenda.PedagogID == (int)HttpContext.Session.GetInt32("PedagogId")).ToList(); //ndryshuar kushti per shkak testimi
            return View(schedules);
        }

        [PedagogCheck]
        [HttpGet("checkin/{id}")]
        public IActionResult CheckIn(int id)
        {

            Schedule schedule = _context.Schedules
                .Include(s => s.ScheduleGrupet)  
                .ThenInclude(sg => sg.Grupi).Include(e=> e.PedagogLenda)  
                .FirstOrDefault(s => s.ScheduleID == id);

            if (schedule.Status == "Krijuar")
            {

                foreach (var scheduleGrupi in schedule.ScheduleGrupet)
                {
                    var grupi = scheduleGrupi.Grupi;


                    var studentsInGroup = _context.Students.Where(s => s.GrupiID == grupi.GrupiID).ToList();

                    foreach (var student in studentsInGroup)
                    {
                        Attendance attendance = new Attendance
                        {
                            ScheduleID = schedule.ScheduleID,
                            StudentID = student.StudentID,
                            LendaId = schedule.PedagogLenda.LendaID,
                            OretZhvilluara = (int)(schedule.EndTime - schedule.StartTime).TotalHours
                    };


                        _context.Add(attendance);
                        _context.SaveChanges();
                    }
                }

                schedule.Status = "Nisur";

                _context.SaveChanges();
            }

            List<Student> studentsInAttendance = _context.Attendances.Where(a => a.ScheduleID == id && a.Status == true) 
                .Include(a => a.Student).ThenInclude(e=> e.Grupi).Select(a => a.Student).ToList();

            ProductViewModel pvm = new ProductViewModel
            {
                Students = studentsInAttendance,
                Schedule = schedule
            };

            return View(pvm); 
        }



        [PedagogCheck]
        [HttpPost("shto/prezencen/{id}")]
        public IActionResult ShtoPrezencen(int id, string RFID)
        {
            var schedule = _context.Schedules
                .Include(e => e.ScheduleGrupet)
                .ThenInclude(e => e.Grupi).Include(e=> e.PedagogLenda)
                .FirstOrDefault(e => e.ScheduleID == id);

            if (schedule == null)
            {
                TempData["Message"] = "Ora nuk eshte gjetur.";
                return RedirectToAction("CheckIn", new { id });
            }

            // Kontrolli i orarit
            if (schedule.EndTime < DateTime.Now)
            {
                TempData["Message"] = "Ora ka kaluar dhe nuk mund te behet check-in.";
                return RedirectToAction("CheckIn", new { id });
            }


            // Marrja e studentit qe ka bere kerkesen
            var student = _context.Students.FirstOrDefault(s => s.RFID == RFID);

            //Kontroll nese studenti tashme e ka tejkaluar numrin e mungesave per kete lende

            if (_context.FailedStudents.Any(e => e.LendaId == schedule.PedagogLenda.LendaID && e.StudentId == student.StudentID))
            {
                TempData["Message"] = "Ju e keni tejkaluar numrin e mungesave per kete lende.";
                return RedirectToAction("CheckIn", new { id });
            }

                if (student == null)
            {
                TempData["Message"] = "Studenti me kete RFID nuk eshte i regjistruar.";
                return RedirectToAction("CheckIn", new { id });
            }

            // Kontrolli i gjendjes se kartes se studentit
            if (student.KartaStatus == false)
            {
                TempData["Message"] = "Karta juaj eshte pezulluar ndaj nuk mund te behet check-in. Ju lutem komunikoni me sekretarine mesimore";
                return RedirectToAction("CheckIn", new { id });
            }

            // Kontrollohet nese studenti eshte pjese e mesimit
            var scheduleGrupi = schedule.ScheduleGrupet
                .Any(e => e.GrupiID == student.GrupiID);

            if (!scheduleGrupi)
            {
                TempData["Message"] = "Studenti nuk i përket grupit të orës aktuale.";
                return RedirectToAction("CheckIn", new { id });
            }

            // Modifikimi i prezences
            Attendance attendance = _context.Attendances.FirstOrDefault(e => e.ScheduleID == id && e.StudentID == student.StudentID);
            attendance.Status = true;

            _context.SaveChanges();

            TempData["Message"] = "Check-in u krye me sukses!";
            return RedirectToAction("CheckIn", new { id });
        }

        [HttpGet("mbylloren/{id}")]
        public IActionResult MbyllOren(int id)
        {

            var schedule = _context.Schedules
                .Include(s => s.ScheduleGrupet).Include(s=> s.PedagogLenda)
                .FirstOrDefault(s => s.ScheduleID == id);

            
            var currentTime = DateTime.Now;
            if (currentTime > schedule.EndTime.AddMinutes(-15))
            {
                TempData["Message"] = "Ora e mesimit nuk mund te mbyllet me shumë se 15 minuta para perfundimit.";
                return RedirectToAction("CheckIn", new { id = id });
            }

            schedule.Status = "Perfunduar";

            int limitiMungesave = _pedagog.KufiriMungesave(schedule.PedagogLenda.LendaID);

            List<int> studentsOverLimit = _context.Attendances.Where(e => e.LendaId == schedule.PedagogLenda.LendaID && !e.Status).GroupBy(e => e.StudentID)
       .Where(e => e.Sum(e => e.OretZhvilluara ?? 0) > limitiMungesave) .Select(e => e.Key).ToList();

            
            List<Student> studentsToNotify = _context.Students
                .Where(s => studentsOverLimit.Contains(s.StudentID))
                .ToList();

            foreach (var student in studentsToNotify)
            {
               /* SendEmail(student.Email, "Njoftim për mungesat", $@"
            I dashur {student.FirstName} {student.LastName},
            Ju keni tejkaluar numrin e lejuar të mungesave për lëndën {schedule.PedagogLenda.Lenda.Emri}.
            Ju lutemi kontaktoni pedagogun tuaj për detaje të mëtejshme.
        ");*/        //per tu implementuar
            }


            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [PedagogCheck]
        [HttpGet("saktesoprezencen/{id}")]
        public IActionResult SaktesoPrezencen(int id)
        {
            
            Schedule schedule = _context.Schedules.Include(e=> e.PedagogLenda).ThenInclude(e=> e.Lenda).FirstOrDefault(s => s.ScheduleID == id);

            if (schedule == null)
            {
                TempData["Message"] = "Ora e mesimit nuk u gjet.";
                return RedirectToAction("Index");
            }


            var currentDate = DateTime.Now.Date; 
           /* if (schedule.EndTime.Date != currentDate)
            {
                TempData["Message"] = "Ora e mesimit nuk mund te saktesohet sepse ka perfunduar afati.";
                return RedirectToAction("Index");
            }*/


            List<Attendance> attendances = _context.Attendances.Where(a => a.ScheduleID == id).Include(a => a.Student).ThenInclude(s => s.Grupi)
         .OrderBy(a => a.Student.Grupi.Viti) .ThenBy(a => a.Student.Grupi.Paraleli).ToList();


           // var studentet = attendances.GroupBy(a => a.Student.Grupi).ToDictionary(g => g.Key, g => g.Select(a => a.Student).ToList());

            ProductViewModel pvm = new ProductViewModel
            {
                Attendances = attendances,
                Schedule = schedule
            };

            return View(pvm); 
        }


    }
}

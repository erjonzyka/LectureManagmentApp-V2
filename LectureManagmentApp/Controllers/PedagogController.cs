using LectureAppLibrary.Interfaces;
using LectureAppLibrary;
using Microsoft.AspNetCore.Mvc;
using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using LectureAppLibrary.Services;

namespace LectureManagmentApp.Controllers
{
    public class PedagogController : Controller
    {

        private readonly ILogger<PedagogController> _logger;

        public ISecretaryService _secretary { get; }
        public IPedagogService _pedagog { get; }
        public EmailService _email { get; }
        public IWebHostEnvironment _environment { get; }
        public MyContext _context { get; }

        public PedagogController(ILogger<PedagogController> logger, ISecretaryService secretary, IPedagogService pedagog, IWebHostEnvironment environment,EmailService email ,MyContext context)
        {
            _logger = logger;
            _secretary = secretary;
            _pedagog = pedagog;
            _environment = environment;
            _context = context;
            _email = email;
        }

        [PedagogCheck]
        public IActionResult Index()
        {
            List<Schedule> schedules = _context.Schedules.Include(e => e.PedagogLenda)
                .ThenInclude(e => e.Lenda)
                .Include(e => e.PedagogLenda.Pedagog)
                .Include(e => e.Classroom)
                .Where(e => e.StartTime.Date == DateTime.Today && e.PedagogLenda.PedagogID == (int)HttpContext.Session.GetInt32("PedagogId")).ToList(); //ndryshuar kushti per shkak testimi
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
        public async Task<IActionResult> MbyllOren(int id)
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
                bool alreadyFailed = _context.FailedStudents.Any(fs =>
                   fs.StudentId == student.StudentID && fs.LendaId == schedule.PedagogLenda.LendaID);
                if (!alreadyFailed)
                {
                    FailedStudent? fs = new FailedStudent
                    {
                        StudentId = student.StudentID,
                        LendaId = schedule.PedagogLenda.LendaID
                    };
                    _context.Add(fs);

                    await _email.SendEmailAsync(
                student.Email,
                "Njoftim për mungesat",
                $@"
                I nderuar {student.FirstName} {student.LastName},
                Ju keni tejkaluar numrin e lejuar te mungesave per lenden {schedule.PedagogLenda.Lenda.EmriLendes}.
                Ju lutemi kontaktoni pedagogun tuaj per detaje te metejshme.");
                }
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
            if (schedule.EndTime.Date != currentDate)
            {
                TempData["Message"] = "Ora e mesimit nuk mund te saktesohet sepse ka perfunduar afati.";
                return RedirectToAction("Index");
            }


            List<Attendance> attendances = _context.Attendances.Where(a => a.ScheduleID == id).Include(a => a.Student).ThenInclude(s => s.Grupi)
         .OrderBy(a => a.Student.Grupi.Viti) .ThenBy(a => a.Student.Grupi.Paraleli).ToList();



            ProductViewModel pvm = new ProductViewModel
            {
                Attendances = attendances,
                Schedule = schedule
            };

            return View(pvm); 
        }


        [PedagogCheck]
        [HttpPost]
        public async Task<IActionResult> KonfirmoPrezencen(int id, List<int> PresentStudents)
        {
            
            Schedule schedule = _context.Schedules
                .Include(s => s.PedagogLenda)
                .FirstOrDefault(s => s.ScheduleID == id);

            if (schedule == null)
            {
                TempData["Message"] = "Orari nuk u gjet.";
                return RedirectToAction("Index");
            }

            // Marrim prezencat e studenteve
            List<Attendance> allAttendances = _context.Attendances
                .Where(a => a.ScheduleID == id)
                .ToList();

            // Perditesojme statusin e prezences
            foreach (var attendance in allAttendances)
            {
                attendance.Status = PresentStudents.Contains(attendance.StudentID);  //nqs lista e permban studentId e shenon prezencen si true, ne te kundert false
            }
            _context.SaveChanges();

            // Marrim oret limit qe mund te mungojne per lenden
            int limitiMungesave = _pedagog.KufiriMungesave(schedule.PedagogLenda.LendaID);

            // Lista e studenteve qe e kane kaluar limitin, vetem id e tyre
            List<int> studentsOverLimit = _context.Attendances
                .Where(a => a.LendaId == schedule.PedagogLenda.LendaID && !a.Status)
                .GroupBy(a => a.StudentID)
                .Where(g => g.Sum(a => a.OretZhvilluara ?? 0) > limitiMungesave)
                .Select(g => g.Key)
                .ToList();

            // Perditesimi ne tabelen FailedStudent
            List<FailedStudent> failedStudents = _context.FailedStudents.Where(fs => fs.LendaId == schedule.PedagogLenda.LendaID).ToList();

            foreach (var failedStudent in failedStudents)
            {
                // Heqim nga tabela studentet qe nuk jane me te ngelur
                if (!studentsOverLimit.Contains(failedStudent.StudentId))
                {
                    _context.FailedStudents.Remove(failedStudent);
                }
            }

            // Shtojme studentet qe kane ngelur pas perditesimit
            foreach (var studentId in studentsOverLimit)
            {
                bool alreadyFailed = _context.FailedStudents.Any(fs =>
                    fs.StudentId == studentId && fs.LendaId == schedule.PedagogLenda.LendaID);
                // nqs nuk ka qen i ngelur para perditesimit shtohet ne tabele
                if (!alreadyFailed)
                {
                    _context.FailedStudents.Add(new FailedStudent
                    {
                        StudentId = studentId,
                        LendaId = schedule.PedagogLenda.LendaID
                    });

                     Student? student = _context.Students.FirstOrDefault(s => s.StudentID == studentId);

                    await _email.SendEmailAsync(
                 student.Email,
                 "Njoftim për mungesat",
                 $@"
                I nderuar {student.FirstName} {student.LastName},
                Ju keni tejkaluar numrin e lejuar te mungesave per lëndën {schedule.PedagogLenda.Lenda.EmriLendes}.
                Ju lutemi kontaktoni pedagogun tuaj per detaje te metejshme.");



                }
            }

            // Ruaj ndryshimet
            _context.SaveChanges();

            TempData["Message"] = "Prezenca u perditesua me sukses.";
            return RedirectToAction("Index");
        }


    }
}

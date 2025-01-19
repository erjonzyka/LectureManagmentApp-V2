using LectureAppLibrary.Interfaces;
using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Services
{
    public class AdminService : IAdminService
    {
        private MyContext _context;

        public AdminService(MyContext context)
        {
            _context = context;
        } 

        public bool ShtoDepartament(Department d)
        {
            _context.Add(d);
            return Save();
        }

        public List<Department> TeGjithaDepartamentet()
        {
            return _context.Departmentet.ToList();
        }

        public bool FshiDepartament(int deptid)
        {
            Department? dept = _context.Departmentet.FirstOrDefault(e => e.DepartmentID == deptid);
            List<Pedagog> pedagoget = _context.Pedagoget.Where(e => e.DepartmentID == deptid).ToList();
            foreach (var pedagog in pedagoget)
            {
                pedagog.DepartmentID = null;
            }
            _context.Remove(dept);
            return Save();
        }

        public List<Pedagog> ShfaqPedagoget()
        {
            return _context.Pedagoget.Include(e=> e.Department).ToList();
        }

        public Pedagog GjejPedagog(int id)
        {
            return _context.Pedagoget.Include(e=> e.PedagogLenda).ThenInclude(e=> e.Lenda).FirstOrDefault(e => e.PedagogID == id);
        }

        public bool RuajNdryshimetPedagog(Pedagog p, int id)
        {
            Pedagog? paraUpdate = _context.Pedagoget.FirstOrDefault(e => e.PedagogID == id);
            paraUpdate.FirstName = p.FirstName;
            paraUpdate.LastName = p.LastName;
            if (paraUpdate.Email != p.Email)
            {
                paraUpdate.Email = p.Email;
            }
            paraUpdate.DepartmentID = p.DepartmentID;
            return Save();
        }

        public List<Lenda> TeGjithaLendet()
        {
            return _context.Lendet.Include(e=> e.Department).Include(e=> e.PedagogLenda).ToList();
        }

        public bool KontrolloLende(string emri)
        {
            return _context.Lendet.Any(e=> e.EmriLendes == emri);
        }

        public bool RuajLende(Lenda lenda)
        {
            _context.Add(lenda);
            return Save();
        }

        public Lenda GjejLenden(int LendaID)
        {
            return _context.Lendet.Include(e => e.Department).Include(e=> e.PedagogLenda).ThenInclude(e=> e.Pedagog).FirstOrDefault(e => e.LendaID == LendaID);
        }

        public bool KontrolloLende(string emri, int i)
        {
            return _context.Lendet.Any(e => e.EmriLendes == emri && e.LendaID != i);
        }

        public bool RuajNdryshimetLenda(Lenda l, int LendaID)
        {
            Lenda? LendaPara = _context.Lendet.FirstOrDefault(e => e.LendaID == LendaID);
            LendaPara.EmriLendes = l.EmriLendes;
            LendaPara.Kredite = l.Kredite;
            LendaPara.OreSeminari = l.OreSeminari;
            LendaPara.OreLeksioni = l.OreLeksioni;
            LendaPara.DepartmentID = l.DepartmentID;
            return Save();
        }

      public List<VitiAkademik> TeGjithaVitetAkademike()
        {
            return _context.VitetAkademike.OrderBy(e=> e.Fillimi).ToList();
        }

        public bool KontrolloPerVitTeHapurAkademik()
        {
            return _context.VitetAkademike.Any(e => e.Flag == 1);
        }

        public VitiAkademik MerrVitinAktual()
        {
            return _context.VitetAkademike.FirstOrDefault(e => e.Flag == 1);
        }

        public VitiAkademik MerrVitinAkademik(int id)
        {
            return _context.VitetAkademike.FirstOrDefault(e => e.Id == id);
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

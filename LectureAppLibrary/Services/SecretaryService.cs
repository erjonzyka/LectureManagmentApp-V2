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
    public class SecretaryService :ISecretaryService
    {

        private MyContext _context;

        public SecretaryService(MyContext context)
        {
            _context = context;
        }





        public List<Grupi> MerrTeGjithaGrupet()
        {
            return _context.Grupet.Include(e=> e.Students).ToList();
        }


        public bool CheckExistence(string paraleli, int viti)
        {
            return _context.Grupet.Any(e => e.Paraleli.ToLower() == paraleli.ToLower() && e.Viti == viti);
        }
            

        public bool FshiGrup(int groupId)
        {
            Grupi? grup = _context.Grupet.FirstOrDefault(e => e.GrupiID == groupId);
            List<Student> studentet = _context.Students.Where(e => e.GrupiID == groupId).ToList();
            foreach (var student in studentet)
            {
                student.GrupiID = null;
            }
            _context.Remove(grup);
            return Save();
        }


        public Grupi GetGrupi(int groupId)
        {
            return _context.Grupet.Include(e => e.Students).FirstOrDefault(e => e.GrupiID == groupId);
        }


        public Student GetStudent(int id)
        {
            return _context.Students.FirstOrDefault(e => e.StudentID == id);
        }

        public List<Student> MerrStudentetPaGrup()
        {
            return _context.Students.Where(e => e.GrupiID == null).OrderBy(e=> e.FirstName).ToList();
        }

        public List<Pedagog> MerrTeGjithePedagoget()
        {
            return _context.Pedagoget.Include(e=> e.PedagogLenda).OrderBy(e => e.FirstName).ToList();
        }

        public List<Lenda> MerrLendetQeNukJep(int pedagoguId)
        {
            var teGjithaLendet = _context.Lendet.ToList();

            var lendetQeJep = _context.PedagogLenda
                .Where(pl => pl.PedagogID == pedagoguId)
                .Select(pl => pl.LendaID)
                .ToList();

            var lendetQeNukJep = teGjithaLendet
                .Where(l => !lendetQeJep.Contains(l.LendaID))
                .ToList();

            return lendetQeNukJep;
        }

        public List<Pedagog> MerrPedagogetQeNukJapin(int lendaId)
        {
            var teGjithePedagoget = _context.Pedagoget.ToList();

            var pedagogetQeJapin = _context.PedagogLenda
                .Where(pl => pl.LendaID == lendaId)
                .Select(pl => pl.PedagogID)
                .ToList();

            var pedagogetQeNukJapin = teGjithePedagoget
                .Where(p => !pedagogetQeJapin.Contains(p.PedagogID))
                .ToList();

            return pedagogetQeNukJapin;
        }

        public bool KontrolloLidhjenPedagogLende(int pedagogId, int lendaId)
        {
            return _context.PedagogLenda.Any(e => e.PedagogID == pedagogId && e.LendaID == lendaId);
        }


        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}

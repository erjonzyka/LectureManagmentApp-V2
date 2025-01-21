using LectureAppLibrary.Interfaces;
using LectureAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Services
{
    public class PedagogService :IPedagogService
    {

        private MyContext _context;

        public PedagogService(MyContext context)
        {
            _context = context;
        }

        public int KufiriMungesave(int lendaId)
        {
            Lenda? lenda = _context.Lendet.FirstOrDefault(e => e.LendaID == lendaId);
            return (int)(0.3 * lenda.OreLeksioni);
        }


    }
}

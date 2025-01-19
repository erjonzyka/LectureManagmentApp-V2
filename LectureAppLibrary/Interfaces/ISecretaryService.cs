using LectureAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Interfaces
{
    public interface ISecretaryService
    {
        List<Grupi> MerrTeGjithaGrupet();
        bool CheckExistence(string paraleli, int viti);
        bool FshiGrup(int groupId);
        Grupi GetGrupi(int groupId);
        Student GetStudent(int studentId);
        List<Student> MerrStudentetPaGrup();
        List<Pedagog> MerrTeGjithePedagoget();
        List<Lenda> MerrLendetQeNukJep(int pedagoguId);
        List<Pedagog> MerrPedagogetQeNukJapin(int lendaId);
        bool KontrolloLidhjenPedagogLende(int pedagogId, int lendaId);
        List<Schedule> GetUpcomingSchedules();
    }
}

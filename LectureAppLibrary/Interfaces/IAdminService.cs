using LectureAppLibrary.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LectureAppLibrary.Interfaces
{
    public interface IAdminService
    {
        bool ShtoDepartament(Department d);
        List<Department> TeGjithaDepartamentet();
        bool FshiDepartament(int deptid);
        List<Pedagog> ShfaqPedagoget();
        Pedagog GjejPedagog(int id);
        bool RuajNdryshimetPedagog(Pedagog pedagog, int id);
        List<Lenda> TeGjithaLendet();
        bool KontrolloLende(string emri);
        bool KontrolloLende(string emri, int id);
        bool RuajLende(Lenda lenda);
        Lenda GjejLenden(int LendaID);
        bool RuajNdryshimetLenda(Lenda l, int LendaID);
        List<VitiAkademik> TeGjithaVitetAkademike();
        bool KontrolloPerVitTeHapurAkademik();
        VitiAkademik MerrVitinAktual();
        VitiAkademik MerrVitinAkademik(int id);
        List<Classroom> GetAllClassrooms();
        bool CheckClassExistence(int SallaNo);
        List<PedagogLenda> MerrPedagogetDheLendet();
    }
}

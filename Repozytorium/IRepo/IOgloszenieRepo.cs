using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.IRepo
{
    public interface IOgloszenieRepo
    {

        IQueryable<Ogloszenie> PobierzOgloszenia();

        IQueryable<Ogloszenie> PobierzStrone(int? page, int? pageSize);

        Ogloszenie GetOgloszenieById(int id);

        void UsunOgloszenie(int id);

        void DodajOgloszenie(Ogloszenie ogloszenie);

        void Aktualizuj(Ogloszenie ogloszenie);

        void SaveChanges();
    }
}

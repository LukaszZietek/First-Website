using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repozytorium.Models;

namespace Repozytorium.IRepo
{
    public interface IBladRepo
    {
        IEnumerable<Bledy> PobierzBledy();

        IEnumerable<Bledy> PobierzOkresloneBledy(string Id);

        Bledy GetBladById(int id);

        void DodajBlad(Bledy blad);

        void SaveChanges();
    }
}

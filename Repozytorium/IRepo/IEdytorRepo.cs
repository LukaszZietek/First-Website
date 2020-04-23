using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repozytorium.Models;

namespace Repozytorium.IRepo
{
    public interface IEdytorRepo
    {
        Edytor EdytujTresc(string userId);

        void EdytujTresc(string userId, Edytor edytor);
        void SaveChanges();

    }
}

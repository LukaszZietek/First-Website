using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Repozytorium.IRepo;
using Repozytorium.Models;

namespace Repozytorium.Repo
{
    public class BladRepo : IBladRepo
    {
        private readonly IOglContext _db;

        public BladRepo(IOglContext db)
        {
            _db = db;
        }

        public IEnumerable<Bledy> PobierzBledy()
        {
            var bledy = _db.Bledy.AsNoTracking();
            return bledy;
        }

        public IEnumerable<Bledy> PobierzOkresloneBledy(string Id)
        {
            var bledy = _db.Bledy.Where(x => x.UzytkownikId == Id).AsNoTracking();
            return bledy;
        }

        public Bledy GetBladById(int id)
        {
            var blad = _db.Bledy.Where(x => x.Id == id).FirstOrDefault();
            return blad;
        }

        public void DodajBlad(Bledy blad)
        {
            _db.Bledy.Add(blad);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
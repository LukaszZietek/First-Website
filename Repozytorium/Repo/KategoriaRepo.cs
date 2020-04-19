using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Repozytorium.IRepo;
using Repozytorium.Models;

namespace Repozytorium.Repo
{
    public class KategoriaRepo: IKategoriaRepo
    {
        private readonly IOglContext _db;

        public KategoriaRepo(IOglContext db)
        {
            this._db = db;
        }
        public IEnumerable<Kategoria> PobierzKategorie()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var kategorie = _db.Kategorie.AsNoTracking();
            return kategorie;
        }

        public IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenia = 
                from o in _db.Ogloszenia
                join k in _db.Ogloszenie_Kategoria on o.Id equals k.Id
                where k.KategoriaId == id
                select o;
            return ogloszenia;
        }

        public string NazwaDlaKategorii(int id)
        {
            var nazwa = _db.Kategorie.Find(id).Nazwa;
            return nazwa;
        }

    }
}
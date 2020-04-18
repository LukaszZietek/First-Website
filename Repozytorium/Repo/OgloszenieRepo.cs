using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Repozytorium.IRepo;
using Repozytorium.Models;

namespace Repozytorium.Repo
{
    public class OgloszenieRepo : IOgloszenieRepo
    {
        private readonly IOglContext _db;

        public OgloszenieRepo(IOglContext db)
        {
            _db = db;
        }

        public IQueryable<Ogloszenie> PobierzOgloszenia()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Ogloszenia.AsNoTracking();
        }

        public IQueryable<Ogloszenie> PobierzStrone(int? page = 1, int? pageSize = 10)
        {
            var ogloszenia = _db.Ogloszenia
                .OrderByDescending(o => o.DataDodania).Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
            return ogloszenia;
        }

        public Ogloszenie GetOgloszenieById(int id)
        {
            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            return ogloszenie;
        }

        public void UsunOgloszenie(int id)
        {
            UsunPowiazanieOgloszenieKategoria(id);

            Ogloszenie ogloszenie = _db.Ogloszenia.Find(id);
            _db.Ogloszenia.Remove(ogloszenie);
           
        }

        public void DodajOgloszenie(Ogloszenie ogloszenie)
        {
            _db.Ogloszenia.Add(ogloszenie);
        }

        public void Aktualizuj(Ogloszenie ogloszenie)
        {
            _db.Entry(ogloszenie).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        private void UsunPowiazanieOgloszenieKategoria(int idOgloszenia)
        {
            var list = _db.Ogloszenie_Kategoria.Where(o => o.OgloszenieId == idOgloszenia);
            foreach (var el in list)
            {
                _db.Ogloszenie_Kategoria.Remove(el);
            }
        }
    }
}
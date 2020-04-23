using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Repozytorium.IRepo;

namespace Repozytorium.Models
{
    public class EdytorRepo:IEdytorRepo
    {
        private readonly IOglContext _db;

        public EdytorRepo(IOglContext db)
        {
            _db = db;
        }

        public Edytor EdytujTresc(string userId)
        {
            Edytor edytor = _db.Edytor.Where(x => x.Id == userId).FirstOrDefault();
            return edytor;
        }

        public void EdytujTresc(string userId, Edytor edytor)
        {
            if (_db.Edytor.Where(x => x.Id == userId).Any())
            {
                _db.Entry(edytor).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                _db.Edytor.Add(edytor);
            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
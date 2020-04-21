using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.IRepo;
using Repozytorium.Models;
using Repozytorium.Models.Views;

namespace OGL.Controllers
{
    public class KategoriaController : Controller
    {
        private readonly IKategoriaRepo _repo;

        public KategoriaController(IKategoriaRepo repo)
        {
            _repo = repo;
        }

        // GET: Kategoria
        public ActionResult Index()
        {
            var kategorie = _repo.PobierzKategorie();
            return View(kategorie);
        }


        public ActionResult Lista()
        {
            return View();
        }
        public ActionResult PokazOgloszenia(int id)
        {
            var ogloszenia = _repo.PobierzOgloszeniaZKategorii(id);
            OgloszeniaZKategoriiViewModel model = new OgloszeniaZKategoriiViewModel();
            model.Ogloszenia = ogloszenia.ToList();
            model.NazwaKategorii = _repo.NazwaDlaKategorii(id);
            return View(model);
        }

        [Route("JSON")]
        public ActionResult KategorieWJson()
        {
            var kategorie = _repo.PobierzKategorie();
            return Json(kategorie, JsonRequestBehavior.AllowGet);
        }



        //// GET: Kategoria/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kategoria kategoria = db.Kategorie.Find(id);
        //    if (kategoria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kategoria);
        //}

        //// GET: Kategoria/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Kategoria/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Nazwa,ParentId,MetaTytul,MetaOpis,MetaSlowa,Tresc")] Kategoria kategoria)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Kategorie.Add(kategoria);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(kategoria);
        //}

        //// GET: Kategoria/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kategoria kategoria = db.Kategorie.Find(id);
        //    if (kategoria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kategoria);
        //}

        //// POST: Kategoria/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Nazwa,ParentId,MetaTytul,MetaOpis,MetaSlowa,Tresc")] Kategoria kategoria)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(kategoria).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(kategoria);
        //}

        //// GET: Kategoria/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kategoria kategoria = db.Kategorie.Find(id);
        //    if (kategoria == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kategoria);
        //}

        //// POST: Kategoria/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Kategoria kategoria = db.Kategorie.Find(id);
        //    db.Kategorie.Remove(kategoria);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

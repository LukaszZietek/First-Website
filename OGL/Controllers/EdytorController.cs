using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ganss.XSS;
using Microsoft.AspNet.Identity;
using Repozytorium.IRepo;
using Repozytorium.Models;

namespace OGL.Controllers
{
    [Authorize]
    public class EdytorController : Controller
    {
        private readonly IEdytorRepo _repo;

        public EdytorController(IEdytorRepo repo)
        {
            _repo = repo;
        }
        // GET: Edytor
        public ActionResult EdytujTresc()
        {
            return View(_repo.EdytujTresc(User.Identity.GetUserId()));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EdytujTresc([Bind(Include = "Id,Tresc")] Edytor edytor)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
               
                    var sanitizer = new HtmlSanitizer();
                    var trescSprawdzona = sanitizer.Sanitize(edytor.Tresc);
                    edytor.Tresc = trescSprawdzona;
                    edytor.Id = userId;
                    try
                    {
                        _repo.EdytujTresc(userId, edytor);
                        _repo.SaveChanges();
                        return RedirectToAction("EdytujTresc", new { edytowanoDodanoInfo = 2 });
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                  
                

            }

            ViewBag.Warning = "Coś poszło nie tak - spróbuj ponownie";
            return View(edytor);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Repozytorium.IRepo;
using Microsoft.AspNet.Identity;
using Repozytorium.Models;

namespace OGL.Controllers
{
    public class BladController : Controller
    {
        private readonly IBladRepo _repo;

        public BladController(IBladRepo repo)
        {
            _repo = repo;
        }


        [Authorize]
        public ActionResult ZglaszanieBledu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ZglaszanieBledu([Bind(Include = "Id,Tresc")] Bledy bledy)
        {
            if (ModelState.IsValid)
            {
                bledy.UzytkownikId = User.Identity.GetUserId();
                bledy.DataDodania = DateTime.Now;
                try
                {
                    _repo.DodajBlad(bledy);
                    _repo.SaveChanges();
                    return RedirectToAction("MojeBledy");
                }
                catch
                {
                    return View(bledy);
                }

            }

            return View(bledy);
        }

        [Authorize]
        public ActionResult MojeBledy()
        {
            var uzytkownik = User.Identity.GetUserId();
            var bledy = _repo.PobierzOkresloneBledy(uzytkownik);
            return View(bledy);
        }


        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

    }
}
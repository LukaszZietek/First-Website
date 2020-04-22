using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Repozytorium.IRepo;
using Repozytorium.Models;
using Services;

namespace OGL.Controllers
{
    public class GaleriaController : Controller
    {
        private IZdjecieRepo _zdjecieRepo;

        public GaleriaController(IZdjecieRepo zdjecieRepo)
        {
            _zdjecieRepo = zdjecieRepo;
        }

        

        public ActionResult Lista()
        {
            List<Zdjecie> zdjecia = _zdjecieRepo.GetAllImages(User.Identity.GetUserId());
            return View(zdjecia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase fileBase)
        {
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                try
                {

               
                ImageUpload imageUpload = new ImageUpload();
                string nameWithExtension = imageUpload.UploadImageAndReturnImageName(fileBase);

                if (nameWithExtension == null)
                    return RedirectToAction("Lista", "Galeria");

                    try
                    {
                        Zdjecie img = new Zdjecie()
                        {
                            UzytkownikId = User.Identity.GetUserId(),
                            Name = nameWithExtension
                        };
                        _zdjecieRepo.AddImage(img);
                        _zdjecieRepo.SaveChanges();
                    }
                    catch
                    {
                        imageUpload.DeleteImageByNameWithMiniatures(nameWithExtension);
                    }
                }
                catch
                {
                    
                }

            }

            return RedirectToAction("Lista", "Galeria");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteImage(string blobName)
        {
            if (blobName == null)
            {
                return false;
            }

            try
            {
                ImageUpload imgUpload = new ImageUpload();
                imgUpload.DeleteImageByNameWithMiniatures(blobName);
                try
                {
                    _zdjecieRepo.DeleteImage(blobName);
                    _zdjecieRepo.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            catch
            {
                
            }

            return false;
        }
    }
}
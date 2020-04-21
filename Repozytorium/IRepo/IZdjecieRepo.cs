using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repozytorium.Models;

namespace Repozytorium.IRepo
{
    public interface IZdjecieRepo
    {
        void AddImage(Zdjecie img);
        void DeleteImage(string blobName);

        List<Zdjecie> GetAllImages(string userId);
        void SaveChanges();

    }
}

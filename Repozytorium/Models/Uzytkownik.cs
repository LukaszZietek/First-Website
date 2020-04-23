using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repozytorium.Models
{ 
        public class Uzytkownik : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Uzytkownik> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }

            public Uzytkownik()
            {
                this.Ogloszenia = new HashSet<Ogloszenie>();
                this.Zdjecia = new HashSet<Zdjecie>();
            }

            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public int ? Wiek { get; set; }

            #region dodatkowe pole notmapped

            [NotMapped]
            [Display(Name = "Pan/Pani: ")]
            public string PelneNaziwsko
            {
                get { return Imie + " " + Nazwisko; }
            }
            #endregion

            public virtual ICollection<Ogloszenie> Ogloszenia { get; private set; }

            public virtual ICollection<Zdjecie> Zdjecia { get; private set; }

            public virtual Edytor Edytor { get; set; }

            

    }
    
}
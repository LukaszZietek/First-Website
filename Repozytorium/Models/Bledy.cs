using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class Bledy
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Treść błędu: ")]
        public string TrescBledu { get; set; }

        
        [Display(Name = "Data dodania: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DataDodania { get; set; }

        [ForeignKey("Uzytkownik")]
        public string UzytkownikId { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; }
    }
}
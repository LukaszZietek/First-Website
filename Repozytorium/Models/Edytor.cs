﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repozytorium.Models
{
    public class Edytor
    {
        [Key,ForeignKey("Uzytkownik")]
        public string Id { get; set; }

        [AllowHtml]
        public string Tresc { get; set; }

        public Uzytkownik Uzytkownik { get; set; }
    }
}
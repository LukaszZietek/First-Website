﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.Models.Views
{
    public class OgloszeniaZKategoriiViewModel
    {
        public IList<Ogloszenie> Ogloszenia { get; set; }
        public string NazwaKategorii { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class NintendoWiiUController : NintendoController
    {
        //
        // GET: /Nintendo/NintendoWiiU/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Nintendo Wii U", page);
        }

    }
}

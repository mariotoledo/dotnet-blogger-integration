using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class MarioController : NintendoController
    {
        //
        // GET: /Nintendo/Retrô/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Mario", page);
        }

    }
}

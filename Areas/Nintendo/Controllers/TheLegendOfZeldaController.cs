using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class TheLegendOfZeldaController : NintendoController
    {
        //
        // GET: /Nintendo/TheLegendOfZelda/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("The Legend of Zelda", page);
        }

    }
}

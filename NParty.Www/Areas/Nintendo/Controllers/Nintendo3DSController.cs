using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class Nintendo3DSController : NintendoController
    {
        //
        // GET: /Nintendo/Nintendo3DS/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Nintendo 3DS", page);
        }

    }
}

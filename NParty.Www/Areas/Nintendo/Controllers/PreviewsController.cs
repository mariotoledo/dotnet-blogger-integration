using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class PreviewsController : NintendoController
    {
        //
        // GET: /Nintendo/Previews/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Preview", page);
        }

    }
}

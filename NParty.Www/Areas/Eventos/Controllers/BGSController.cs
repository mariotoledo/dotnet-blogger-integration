using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos.Controllers
{
    public class BGSController : EventosController
    {
        //
        // GET: /Eventos/BGS/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("BGS", page);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos.Controllers
{
    public class CoberturasController : EventosController
    {
        //
        // GET: /Eventos/Coberturas/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Cobertura", page);
        }

    }
}

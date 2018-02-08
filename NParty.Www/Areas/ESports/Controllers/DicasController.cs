using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class DicasController : ESportsController
    {
        //
        // GET: /ESports/Dicas/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Dicas", page);
        }

    }
}

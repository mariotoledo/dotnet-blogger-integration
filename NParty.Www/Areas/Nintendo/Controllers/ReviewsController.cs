using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class ReviewsController : NintendoController
    {
        //
        // GET: /Nintendo/Reviews/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Review", page);
        }

    }
}

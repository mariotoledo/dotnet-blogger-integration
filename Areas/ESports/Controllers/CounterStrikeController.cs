using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class CounterStrikeController : ESportsController
    {
        //
        // GET: /ESports/CounterStrike/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Counter Strike", page);
        }

    }
}

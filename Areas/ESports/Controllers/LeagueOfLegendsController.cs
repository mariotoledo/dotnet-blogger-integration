using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class LeagueOfLegendsController : ESportsController
    {
        //
        // GET: /ESports/LeagueOfLegends/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("League of Legends", page);
        }

    }
}

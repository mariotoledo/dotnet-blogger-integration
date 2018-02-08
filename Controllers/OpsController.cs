using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class OpsController : Controller
    {
        //
        // GET: /Ops/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NaoEncontrado()
        {
            return View();
        }
    }
}

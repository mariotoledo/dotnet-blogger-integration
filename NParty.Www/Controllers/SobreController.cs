using Google.GData.Client;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class SobreController : NPartyController
    {
        public ActionResult QuemSomos() { return View(); }
        public ActionResult Contato() { return View(); }
        public ActionResult FacaParte() { return View(); }
    }
}

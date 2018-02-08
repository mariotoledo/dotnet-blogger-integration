using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Eventos.Controllers
{
    public class AnimeFriendsController : EventosController
    {
        //
        // GET: /Eventos/AnimeFriends/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Anime Friends", page);
        }

    }
}

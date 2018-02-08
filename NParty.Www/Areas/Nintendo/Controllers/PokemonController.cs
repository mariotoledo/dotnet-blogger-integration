using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Nintendo.Controllers
{
    public class PokemonController : NintendoController
    {
        //
        // GET: /Nintendo/Pokemon/

        public ActionResult Index(int? page)
        {
            return OpenPageByLabel("Pokémon", page);
        }

        public ActionResult Timeline() { return View(); }

        public ActionResult PokePartyDay() { return View(); }

    }
}

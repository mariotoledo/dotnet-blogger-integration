using NParty.Www.Controllers;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Podcasts.Controllers
{
    public class NintendoA3Controller : PodcastsController
    {
        //
        // GET: /Podcasts/NintendoA3/

        public ActionResult Ouvir(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("~/Podcasts/Home/Index");
            }

            Article article = Helper.GetSingleArticleFromBlog(NintendoBlogId, "Podcasts", id);

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

            ViewData["article"] = article;

            return View();
        }

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            List<Article> nintendoa3Articles = Helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 20, (20 * pageValue) + 1, "Nintendo a 3", "/Artigos/Ler/");

            ViewData["articles"] = nintendoa3Articles;
            ViewData["currentPage"] = pageValue;

            return View();
        }
    }
}
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
    public class NPartyCostaACostaController : PodcastsController
    {
        //
        // GET: /Podcasts/NPartyCostaACosta/

        public ActionResult Ouvir(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("~/Podcasts/Home/Index");
            }

            Article article = Helper.GetSingleArticleFromBlog(MainBlogId, "Podcasts", id);

            if (article == null)
                return Redirect("~/Ops/NaoEncontrado");

            ViewData["article"] = article;

            return View();
        }

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            List<Article> costaACostaArticles = Helper.GetArticlesFromBlog(MainBlogId, "Podcasts", 20, (20 * pageValue) + 1, "N-Party Costa a Costa", "/NPartyCostaACosta/Ouvir/");

            ViewData["articles"] = costaACostaArticles;
            ViewData["currentPage"] = pageValue;

            return View();
        }

    }
}
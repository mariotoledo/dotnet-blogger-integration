using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.Podcasts.Controllers
{
    public class ArtigosController : PodcastsController
    {
        //
        // GET: /Podcasts/Artigos/

        public ActionResult Ler(string id)
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

    }
}

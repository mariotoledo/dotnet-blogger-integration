﻿using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class HomeController : ESportsController
    {
        //
        // GET: /ESports/Home/

        public ActionResult Index(int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            List<Article> nintendoArticles = Helper.GetArticlesFromBlog(ESportsBlogId, "ESports", MaxPosts, (MaxPosts * pageValue) + 1, "/Artigos/Ler/");
            List<Article> hilights = Helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 5, 0, "Destaque", "/Artigos/Ler/");

            ViewData["articles"] = nintendoArticles;
            ViewData["hilights"] = hilights;
            ViewData["currentPage"] = pageValue;

            return View();
        }

    }
}

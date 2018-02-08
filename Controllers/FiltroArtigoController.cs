using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class FiltroArtigoController : Controller
    {
        //
        // GET: /FiltroArtigo/

        public ActionResult Index()
        {
            ArticlesHelper articlesHelper = new ArticlesHelper();

            List<Article> articles = articlesHelper.GetArticlesFromBlog(System.Configuration.ConfigurationManager.AppSettings["nintendoBlogId"], "Nintendo", 10000, 0, "/Artigos/Ler/");
            return View(articles);
        }

        public ActionResult Autor(string id)
        {
            ArticlesHelper articlesHelper = new ArticlesHelper();

            List<Article> articles = articlesHelper.GetArticlesFromBlog(System.Configuration.ConfigurationManager.AppSettings["nintendoBlogId"], "Nintendo", 10000, 0, "/Artigos/Ler/");

            articles.RemoveAll(t => t.AuthorName != id);

            return View(articles);
        }

    }
}

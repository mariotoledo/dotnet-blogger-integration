using Google.Apis.Blogger.v3;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class E3Controller : NPartyController
    {
        //
        // GET: /E3/

        public ActionResult Index()
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["BloggerApiKey"];
            string appName = System.Configuration.ConfigurationManager.AppSettings["GoogleAppName"];

            BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName  = appName
            });

            ArticlesHelper helper = new ArticlesHelper(apiKey, appName, service);

            List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 100, 1, "E3 2016", "/Artigos/Ler/");
            List<Article> generalArticles = helper.GetArticlesFromBlog(MainBlogId, "Multi", 100, 1, "E3 2016", "/Artigos/Ler/");

            List<Article> articles = new List<Article>();
            articles.AddRange(nintendoArticles);
            articles.AddRange(generalArticles);

            ViewData["articles"] = articles.OrderByDescending(t => t.DatePublished).ToList();

            return View();
        }

    }
}

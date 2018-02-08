using Google.Apis.Blogger.v3;
using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Areas.ESports.Controllers
{
    public class EVOController : ESportsController
    {
        //
        // GET: /ESports/EVO/

        public ActionResult Index(int? page)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["ESportsBloggerApiKey"];
            string appName = System.Configuration.ConfigurationManager.AppSettings["ESportsGoogleAppName"];

            BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = appName
            });

            ArticlesHelper helper = new ArticlesHelper(apiKey, appName, service);

            List<Article> generalArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 100, 1, "EVO 2016", "/Artigos/Ler/");

            ViewData["articles"] = generalArticles;

            return View();
        }

    }
}

using Google.Apis.Blogger.v3;
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
    public class PodcastsController : NPartyController
    {
        //
        // GET: /Podcasts/Podcasts/

        protected ActionResult OpenPageByLabel(string label, int? page)
        {
            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            List<Article> articles = Helper.GetArticlesFromBlog(MainBlogId, "Podcasts", MaxPosts, (MaxPosts * pageValue) + 1, label, "/Ouvir/");

            ViewData["articles"] = articles;
            ViewData["currentPage"] = pageValue;

            return View();
        }

        private static string apiKey = System.Configuration.ConfigurationManager.AppSettings["PodcastBloggerApiKey"];
        private static string appName = System.Configuration.ConfigurationManager.AppSettings["PodcastGoogleAppName"];

        private static volatile BloggerService _service;

        protected static BloggerService Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
                    {
                        ApiKey = apiKey,
                        ApplicationName = appName
                    });
                }

                return _service;
            }
        }

        private static volatile ArticlesHelper _helper;

        protected static ArticlesHelper Helper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new ArticlesHelper(apiKey, appName, Service);
                }

                return _helper;
            }
        }
    }
}
using Google.Apis.Blogger.v3;
using Google.GData.Client;
using NParty.Www.Helpers;
using NParty.Www.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace NParty.Www.Controllers
{
    public class HomeController : NPartyController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            NPartyArticlesHelper helper = new NPartyArticlesHelper();

            Dictionary<string, string> blogDomains = new Dictionary<string, string>();
            blogDomains.Add(NintendoBlogId, "Nintendo");
            blogDomains.Add(ESportsBlogId, "ESports");
            blogDomains.Add(EventosBlogId, "Eventos");

            List<Article> hilights = helper.GetHomeHilights(blogDomains, MainBlogId);
            List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 4, "/Artigos/Ler/");
            List<Article> esportsArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", 4, "/Artigos/Ler/");
            List<Article> eventosArticles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", 4, "/Artigos/Ler/");

            ViewData["Hilights"] = hilights;
            ViewData["NintendoPosts"] = nintendoArticles;
            ViewData["eSportsPosts"] = esportsArticles;
            ViewData["EventosPosts"] = eventosArticles;

            return View();
        }

        [HttpGet]
        public ActionResult Busca(string q, int? page)
        {
            ArticlesHelper helper = new ArticlesHelper();

            int pageValue = page.HasValue && page.Value > 0 ? page.Value : 0;

            ViewData["query"] = q;
            ViewData["currentPage"] = pageValue;
            
            List<Article> nintendoArticles = helper.SearchArticlesInBlog(NintendoBlogId, "Nintendo", 50, (50 * pageValue) + 1, q);
            List<Article> eSportsArticles = helper.SearchArticlesInBlog(ESportsBlogId, "eSports", 50, (50 * pageValue) + 1, q);
            List<Article> eventosArticles = helper.SearchArticlesInBlog(EventosBlogId, "Eventos", 50, (50 * pageValue) + 1, q);

            List<Article> allArticles = new List<Article>();
            allArticles.AddRange(nintendoArticles);
            allArticles.AddRange(eSportsArticles);
            allArticles.AddRange(eventosArticles);

            ViewData["SearchResults"] = allArticles.OrderByDescending(t => t.DatePublished).ToList();

            return View();
        }

        public JsonResult PodcastFeedOnHome()
        {
            NPartyArticlesHelper helper = new NPartyArticlesHelper();

            Dictionary<string, string> blogDomains = new Dictionary<string, string>();
            blogDomains.Add(NintendoBlogId, "Nintendo");
            blogDomains.Add(ESportsBlogId, "NParty");

            List<Article> nintendoPodcasts = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", 4, 0, "Nintendo a 3", "/Artigos/Ler/");
            List<Article> npartyPodcasts = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", 4, 0, "N-Party Costa a Costa", "/NPartyCostaACosta/Ouvir/");

            List<Article> podcastFeed = new List<Article>();
            podcastFeed.AddRange(nintendoPodcasts);
            podcastFeed.AddRange(npartyPodcasts);

            return Json(podcastFeed.OrderByDescending(t => t.DatePublished).Take(4).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ler(string year, string month, string path)
        {
            string appName = System.Configuration.ConfigurationManager.AppSettings["GoogleAppName"];
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["BloggerApiKey"];

            BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = appName
            });

            ArticlesHelper helper = new ArticlesHelper(
                appName,
                apiKey,
                service
            );  

            Article article = helper.GetSingleArticleFromBlogByPath(NintendoBlogId, "Nintendo", "/" + year + "/" + month + "/" + path + ".html");
                
            if (article == null)
            {
                article = helper.GetSingleArticleFromBlogByPath(EventosBlogId, "Eventos", "/" + year + "/" + month + "/" + path + ".html");

                if(article == null)
                {
                    article = helper.GetSingleArticleFromBlogByPath(ESportsBlogId, "ESports", "/" + year + "/" + month + "/" + path + ".html");

                    if(article == null)
                    {
                        return Redirect("~/Ops/NaoEncontrado");
                    }
                }                
            }

            ViewData["article"] = article;

            return View();
        }

        [HttpGet]
        public ActionResult GenerateUrl()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GenerateUrl(string domain, string postId, string title)
        {
            Article a = new Article()
            {
                Domain = domain,
                Id = postId,
                Title = title
            };

            a.GenerateNPartyArticleLink(domain, "/Artigos/Ler/");

            return Json("http://www.nparty.com.br/" + a.NPartyArticleLink, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UrlInvalida()
        {
            return View();
        }

        public JsonResult GetHilightItems()
        {
            NPartyArticlesHelper helper = new NPartyArticlesHelper();

            Dictionary<string, string> blogDomains = new Dictionary<string, string>();
            blogDomains.Add(NintendoBlogId, "Nintendo");
            blogDomains.Add(ESportsBlogId, "ESports");
            blogDomains.Add(EventosBlogId, "Eventos");

            return Json(helper.GetGeneralHilights(blogDomains, 3), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SitesFeed()
        {
            string url = "http://nintendoeverything.com/feed/";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            ViewData["everything"] = feed.Items;

            string url2 = "http://feedpress.me/gonintendo";
            XmlReader reader2 = XmlReader.Create(url2);
            SyndicationFeed feed2 = SyndicationFeed.Load(reader2);
            reader2.Close();

            ViewData["go"] = feed2.Items;

            string url3 = "https://mynintendonews.com/feed/";
            XmlReader reader3 = XmlReader.Create(url3);
            SyndicationFeed feed3 = SyndicationFeed.Load(reader3);
            reader2.Close();

            ViewData["my"] = feed3.Items;

            return View();
        }

        [HttpPost]
        public JsonResult SubscribeEmailToNewsletter(string email)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { status = "error", message = "Você precisa digitar um email para cadastro" }, JsonRequestBehavior.AllowGet);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://us9.api.mailchimp.com/2.0/lists/subscribe.json?apikey=5f0566af2a617830f1f4980003f755be-us9&id=1c5d2516eb&email[email]=" + email + "&double_optin=true&send_welcome=false");

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                JsonObject contentObject = (JsonObject)SimpleJson.DeserializeObject(responseString);

                if (contentObject.ContainsKey("status") && (string)contentObject["status"] == "error")
                {
                    string name = (string)contentObject["name"];
                    int code = (int)contentObject["code"];

                    if (code == -99)
                        return Json(new { status = "error", message = "Você precisa digitar um email válido" }, JsonRequestBehavior.AllowGet);
                    else if(name == "List_AlreadySubscribed")
                        return Json(new { status = "error", message = "Seu email já está cadastrado" }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { status = "error", message = "Ocorreu um erro ao tentar realizar seu registro. Por favor, tente novamente mais tarde." }, JsonRequestBehavior.AllowGet);
                }
                else if (contentObject.ContainsKey("euid"))
                {
                    return Json (new { status = "success", message = "Email cadastrado com sucesso!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "error", message = "Ocorreu um erro ao tentar realizar seu registro. Por favor, tente novamente mais tarde." }, JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e)
            {
                return Json(new { status = "error", message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
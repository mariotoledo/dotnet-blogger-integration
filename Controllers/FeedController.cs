using NParty.Www.Helpers;
using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace NParty.Www.Controllers
{
    public class FeedController : NPartyController
    {
        public ActionResult SiteFeed(string type, int? n)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower().Equals("rss"))
            {
                int totalItems = n.HasValue == false || n.Value <= 0 ? 50 : n.Value;

                NPartyArticlesHelper helper = new NPartyArticlesHelper();

                List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", totalItems, "/Artigos/Ler/");
                List<Article> esportsArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", totalItems, "/Artigos/Ler/");
                List<Article> eventosArticles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", totalItems, "/Artigos/Ler/");
                List<Article> npartyPodcasts = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", totalItems, 0, "N-Party Costa a Costa", "/NPartyCostaACosta/Ouvir/");

                List<Article> articles = new List<Article>();
                articles.AddRange(nintendoArticles);
                articles.AddRange(esportsArticles);
                articles.AddRange(eventosArticles);
                articles.AddRange(npartyPodcasts);

                List<Article> orderedArticles = articles.OrderByDescending(t => t.DatePublished).ToList();

                var items = new List<SyndicationItem>();

                var feed = new SyndicationFeed("N-Party", "O melhor do mundo da Nintendo, eSports e Eventos com a N-Party", new Uri("http://nparty.com.br/Feed/SiteFeed"));

                foreach (Article article in orderedArticles)
                {
                    items.Add(new SyndicationItem(
                        article.Title, 
                        article.Summary, 
                        new Uri("http://nparty.com.br/" +  article.NPartyArticleLink), 
                        article.Id, 
                        article.DatePublished
                    ));
                }

                feed.Items = items;

                return new RssActionResult() { Feed = feed };
            }
            else {
                return Redirect("~/");
            }
        }

        public ActionResult NintendoFeed(string type, int? n)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower().Equals("rss"))
            {
                int totalItems = n.HasValue == false || n.Value <= 0 ? 50 : n.Value;

                NPartyArticlesHelper helper = new NPartyArticlesHelper();

                List<Article> nintendoArticles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", totalItems, "/Artigos/Ler/");

                var items = new List<SyndicationItem>();

                var feed = new SyndicationFeed("Nintendo N-Party", "O melhor do mundo da Nintendo com a N-Party. Wii U, 3DS, Preview, Review, Notícias, Pokémon, Zelda, Mario e mais", new Uri("http://nparty.com.br/Feed/NintendoFeed"));

                foreach (Article article in nintendoArticles)
                {
                    items.Add(new SyndicationItem(
                        article.Title,
                        article.Summary,
                        new Uri("http://nparty.com.br/" + article.NPartyArticleLink),
                        article.Id,
                        article.DatePublished
                    ));
                }

                feed.Items = items;

                return new RssActionResult() { Feed = feed };
            }
            else {
                return Redirect("~/");
            }
        }

        public ActionResult ESportsFeed(string type, int? n)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower().Equals("rss"))
            {
                int totalItems = n.HasValue == false || n.Value <= 0 ? 50 : n.Value;

                NPartyArticlesHelper helper = new NPartyArticlesHelper();

                List<Article> esportsArticles = helper.GetArticlesFromBlog(ESportsBlogId, "ESports", totalItems, "/Artigos/Ler/");

                var items = new List<SyndicationItem>();

                var feed = new SyndicationFeed("eSports N-Party", "O melhor do mundo do eSports com a N-Party. League of Legends, Counter Strike, Dicas e mais", new Uri("http://nparty.com.br/Feed/ESportsFeed"));

                foreach (Article article in esportsArticles)
                {
                    items.Add(new SyndicationItem(
                        article.Title,
                        article.Summary,
                        new Uri("http://nparty.com.br/" + article.NPartyArticleLink),
                        article.Id,
                        article.DatePublished
                    ));
                }

                feed.Items = items;

                return new RssActionResult() { Feed = feed };
            }
            else {
                return Redirect("~/");
            }
        }

        public ActionResult EventosFeed(string type, int? n)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower().Equals("rss"))
            {
                int totalItems = n.HasValue == false || n.Value <= 0 ? 50 : n.Value;

                NPartyArticlesHelper helper = new NPartyArticlesHelper();

                List<Article> eventosArticles = helper.GetArticlesFromBlog(EventosBlogId, "Eventos", totalItems, "/Artigos/Ler/");

                var items = new List<SyndicationItem>();

                var feed = new SyndicationFeed("Eventos N-Party", "O melhor do mundo do eSports com a N-Party. League of Legends, Counter Strike, Dicas e mais", new Uri("http://nparty.com.br/Feed/EventosFeed"));

                foreach (Article article in eventosArticles)
                {
                    items.Add(new SyndicationItem(
                        article.Title,
                        article.Summary,
                        new Uri("http://nparty.com.br/" + article.NPartyArticleLink),
                        article.Id,
                        article.DatePublished
                    ));
                }

                feed.Items = items;

                return new RssActionResult() { Feed = feed };
            }
            else {
                return Redirect("~/");
            }
        }

        public ActionResult PodcastsFeed(string type, int? n)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower().Equals("rss"))
            {
                int totalItems = n.HasValue == false || n.Value <= 0 ? 50 : n.Value;

                NPartyArticlesHelper helper = new NPartyArticlesHelper();

                List<Article> npartyPodcasts = helper.GetArticlesFromBlog(MainBlogId, "Podcasts", totalItems, 0, "N-Party Costa a Costa", "/NPartyCostaACosta/Ouvir/");
                List<Article> nintendoa3Articles = helper.GetArticlesFromBlog(NintendoBlogId, "Nintendo", totalItems, 0, "Nintendo a 3", "/Artigos/Ler/");

                List<Article> articles = new List<Article>();
                articles.AddRange(npartyPodcasts);
                articles.AddRange(nintendoa3Articles);

                List<Article> orderedArticles = articles.OrderByDescending(t => t.DatePublished).ToList();

                var items = new List<SyndicationItem>();

                var feed = new SyndicationFeed("N-Party", "Coloque seus fones de ouvidos e escute uma galera falando o que pensa sobre games e tudo mais!", new Uri("http://nparty.com.br/Feed/PodcastsFeed"));

                foreach (Article article in orderedArticles)
                {
                    SyndicationItem item = new SyndicationItem(
                        article.Title,
                        article.Content,
                        new Uri("http://nparty.com.br/" + article.NPartyArticleLink),
                        article.Id,
                        article.DatePublished);


                    item.PublishDate = article.DatePublished;

                    items.Add(item);
                }

                feed.Items = items;
                feed.Language = "pt-br";

                return new RssActionResult() { Feed = feed };
            }
            else {
                return Redirect("~/");
            }
        }
    }
}
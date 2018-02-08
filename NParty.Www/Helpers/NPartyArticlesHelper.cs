using NParty.Www.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NParty.Www.Helpers
{
    public class NPartyArticlesHelper : ArticlesHelper
    {
        public List<Article> GetHomeHilights(Dictionary<string, string> blogDomains, string mainBlogId)
        {
            List<Article> articles = new List<Article>();

            try
            {
                foreach (string blogId in blogDomains.Keys)
                {
                    articles.AddRange(GetArticlesFromBlog(blogId, blogDomains[blogId], 5, 0, "Destaque", "/Artigos/Ler/"));
                }

                articles.AddRange(GetArticlesFromBlog(mainBlogId, "Podcasts", 4, 0, "Podcast", "/NPartyCostaACosta/Ouvir/"));

                List<Article> mainArticles = GetArticlesFromBlog(mainBlogId, "Multi", 4, 0, "Destaque", "/Artigos/Ler/");
                mainArticles.RemoveAll(t => t.Labels.Contains("Podcast"));

                articles.AddRange(mainArticles);

               articles = (List <Article>)articles.OrderByDescending(c => c.DatePublished).Take(5).ToList();
            } catch (Exception e)
            {

            }

            return articles;
        }

        public List<Article> GetGeneralHilights(Dictionary<string, string> blogDomains, int maxArticles)
        {
            List<Article> articles = new List<Article>();

            try
            {
                foreach (string blogId in blogDomains.Keys)
                {
                    articles.AddRange(GetArticlesFromBlog(blogId, blogDomains[blogId], maxArticles, 0, "Destaque", "/Artigos/Ler/"));
                }

                articles = (List<Article>)articles.OrderByDescending(c => c.DatePublished).ToList();
            }
            catch (Exception e)
            {

            }

            return articles;
        }
    }
}
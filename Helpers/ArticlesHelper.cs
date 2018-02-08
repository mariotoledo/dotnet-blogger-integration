using System;
using System.Collections.Generic;
using Google.Apis.Blogger.v3;
using Google.GData.Client;
using NParty.Www.Models;
using System.Xml;
using System.Text.RegularExpressions;
using Google.Apis.Blogger.v3.Data;
using Newtonsoft.Json;
using System.Web;

namespace NParty.Www.Helpers
{
    public class ArticlesHelper
    {
        private string applicationName;
        private string apiKey;

        private BloggerService service;

        public ArticlesHelper() { }

        public ArticlesHelper(string applicationName, string apiKey, BloggerService service)
        {
            this.applicationName = applicationName;
            this.apiKey = apiKey;
            this.service = service;
        }

        public List<Article> SearchArticlesInBlog(string blogId, string blogDomain, int maxResults, int startIndex, string query)
        {
            List<Article> articles = new List<Article>();
            try
            {
                Service service = new Service("blogger", "n-party");
                string maxResultAppend = maxResults > 0 ? "&max-results=" + maxResults : "";

                AtomFeed feed = service.Query(new FeedQuery()
                {
                    Uri = new Uri("http://www.blogger.com/feeds/" + blogId + "/posts/default?q=" + query + "?start-index=" + startIndex + maxResultAppend)
                });

                foreach (var post in feed.Entries)
                {
                    Article article = new Article();
                    article.Title = post.Title.Text;
                    article.Content = post.Content.Content;

                    article.AuthorName = post.Authors[0].Name;

                    article.DatePublished = post.Published;
                    article.CoverImage = GetPostImageManually(post);
                    article.Summary = GetSummaryManually(post.Content.Content, 200);
                    article.Id = GetPostIdManually(post.Id.AbsoluteUri);
                    article.ArticleLink = GetPostLinkManually(post.Links);
                    article.Labels = GetLabelsArray(post.Categories);
                    article.GenerateNPartyArticleLink(blogDomain, "/Artigos/Ler/");

                    article.Domain = blogDomain;

                    articles.Add(article);
                };
            }
            catch (Exception e)
            {

            }

            return articles;
        }

        internal Article GetSingleArticleFromBlog(object mainBlogId, string v, string id)
        {
            throw new NotImplementedException();
        }

        public Article GetSingleArticleFromBlogByPath(string blogId, string blogDomain, string path)
        {
            Article article = null;
            try
            {
                BloggerService service = new BloggerService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    ApiKey = this.apiKey,
                    ApplicationName = this.applicationName
                });

                PostsResource.GetByPathRequest resource = service.Posts.GetByPath(blogId, path);

                Post post = resource.Execute();

                article = new Article();
                article.Id = post.Id;
                article.Title = post.Title;

                if (post.Author != null)
                {
                    article.AuthorData = GetAuthorFromJson(post.Author);
                    article.AuthorName = post.Author.DisplayName;
                }

                article.Content = post.Content;
                article.CoverImage = post.Images != null && post.Images.Count > 0 ? post.Images[0].Url : null;
                article.DatePublished = post.Published.HasValue ? post.Published.Value : DateTime.MinValue;

                article.Domain = blogDomain;
                article.GenerateNPartyArticleLink(blogDomain, "/Artigos/Ler/");

                if (post.Labels != null && post.Labels.Count > 0)
                {
                    article.Labels = new string[post.Labels.Count];
                    for (int i = 0; i < post.Labels.Count; i++)
                    {
                        article.Labels[i] = post.Labels[i];
                    }
                }
            }
            catch (Exception e)
            {

            }

            return article;
        }

        public Article GetSingleArticleFromBlog(string blogId, string blogDomain, string postId)
        {
            Article article = null;
            try
            {
                PostsResource.GetRequest resource = service.Posts.Get(blogId, postId);
                resource.FetchImages = true;
                resource.FetchBody = true;
                //resource.

                Post post = resource.Execute();

                article = new Article();
                article.Id = postId;
                article.Title = post.Title;

                if (post.Author != null)
                {
                    article.AuthorData = GetAuthorFromJson(post.Author);
                    article.AuthorName = post.Author.DisplayName;
                }

                article.Content = post.Content;
                article.CoverImage = post.Images != null && post.Images.Count > 0 ? post.Images[0].Url : null;
                article.DatePublished = post.Published.HasValue ? post.Published.Value : DateTime.MinValue;

                article.Domain = blogDomain;
                article.GenerateNPartyArticleLink(blogDomain, "/Artigos/Ler/");

                if (post.Labels != null && post.Labels.Count > 0)
                {
                    article.Labels = new string[post.Labels.Count];
                    for (int i = 0; i < post.Labels.Count; i++)
                    {
                        article.Labels[i] = post.Labels[i];
                    }
                }
            }
            catch (Exception e)
            {

            }

            return article;
        }

        public Author GetAuthorFromJson(Post.AuthorData authorData)
        {
            string json = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/json/authors.json"));
            Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            string id = authorData.Id;

            if (values != null && values.ContainsKey(id))
            {
                Author author = new Author();

                author.Id = id;
                author.Name = values[id]["name"];
                author.Description = values[id]["description"];
                author.ImageUrl = values[id]["imageUrl"];
                author.FacebookProfileUrl = values[id].ContainsKey("facebookUrl") ? values[id]["facebookUrl"] : null;
                author.TwitterAccount = values[id].ContainsKey("twitterAccount") ? values[id]["twitterAccount"] : null;

                return author;
            }

            return null;
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults, int startIndex, string postsLabel, string readActionUrl)
        {
            List<Article> articles = new List<Article>();
            try
            {
                Service bloggerService = new Service("blogger", "n-party");

                string labelAppend = string.IsNullOrEmpty(postsLabel) ? "" : "/-/" + postsLabel;
                string maxResultAppend = maxResults > 0 ? "&max-results=" + maxResults : "";

                AtomFeed feed = bloggerService.Query(new FeedQuery()
                {
                    Uri = new Uri("http://www.blogger.com/feeds/" + blogId + "/posts/default" + labelAppend + "?start-index=" + startIndex + maxResultAppend)
                });

                foreach (var post in feed.Entries)
                {
                    Article article = new Article();
                    article.Title = post.Title.Text;
                    article.Content = post.Content.Content;

                    article.AuthorName = post.Authors[0].Name;

                    article.DatePublished = post.Published;
                    article.CoverImage = GetPostImageManually(post);
                    article.Summary = GetSummaryManually(post.Content.Content, 200);
                    article.Id = GetPostIdManually(post.Id.AbsoluteUri);
                    article.ArticleLink = GetPostLinkManually(post.Links);
                    article.Labels = GetLabelsArray(post.Categories);

                    article.Domain = blogDomain;
                    article.GenerateNPartyArticleLink(blogDomain, readActionUrl);

                    articles.Add(article);
                };
            }
            catch (Exception e)
            {

            }

            return articles;
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults, int startIndex, string readActionUrl)
        {
            return GetArticlesFromBlog(blogId, blogDomain, maxResults, startIndex, "", readActionUrl);
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, int maxResults, string readActionUrl)
        {
            return GetArticlesFromBlog(blogId, blogDomain, maxResults, 0, readActionUrl);
        }

        public List<Article> GetArticlesFromBlog(string blogId, string blogDomain, string readActionUrl)
        {
            return GetArticlesFromBlog(blogId, blogDomain, 0, 10, readActionUrl);
        }

        private string[] GetLabelsArray(AtomCategoryCollection postCategories)
        {
            if (postCategories != null && postCategories.Count > 0)
            {
                string[] labels = new string[postCategories.Count];
                for (int i = 0; i < postCategories.Count; i++)
                {
                    labels[i] = postCategories[i].Term;
                }

                return labels;
            }
            else
                return new string[] { };
        }

        private string GetPostImageManually(AtomEntry feedEntryXml)
        {
            string pattern = "<img.+?src=[\"'](.+?)[\"'].*?>";
            MatchCollection matches = Regex.Matches(feedEntryXml.Content.Content, pattern, RegexOptions.IgnoreCase);

            if (matches != null && matches.Count > 0)
                return matches[0].Groups[1].ToString();

            return "";
        }

        private string GetPostIdManually(string originalId)
        {
            string[] splitedId = originalId.Split('-');

            if (splitedId != null && splitedId.Length > 0)
                return splitedId[splitedId.Length - 1];

            return "";
        }

        private string GetPostLinkManually(AtomLinkCollection links)
        {
            foreach (AtomLink link in links)
            {
                if (link.Rel.CompareTo("alternate") == 0)
                    return link.HRef.Content;
            }

            return "";
        }

        private string GetSummaryManually(string htmlContent, int maxLength)
        {
            string result = Regex.Replace(htmlContent, @"<[^>]*>", String.Empty);
            if (result != null)
            {
                result = result.Trim();
                if (result.Length > maxLength)
                {
                    result = result.Substring(0, maxLength);
                }

                result += "...";
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NParty.Www.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public Author AuthorData { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public string ArticleLink { get; set; }
        public string NPartyArticleLink { get; set; }
        public string Domain { get; set; }
        public string Id { get; set; }
        public string[] Labels { get; set; }

        public virtual void GenerateNPartyArticleLink(string domain, string readActionUrl)
        {
            this.NPartyArticleLink = domain + readActionUrl + Id + "/" + GetTitleForUrl();
        }

        public string GetTitleForUrl()
        {
            try
            {
                if (Title != null)
                {
                    string[] splittedTitle = Title.Normalize().ToLower().RemoveSpecialCharacters().RemoveDiacritics().Split(' ');
                    StringBuilder builder = new StringBuilder();
                    for(int i = 0; i < splittedTitle.Length && i < 6; i++)
                    {
                        builder.Append(splittedTitle[i]);
                        builder.Append('-');
                    }

                    string urlTitle = builder.ToString();
                    if (urlTitle.EndsWith("-"))
                        urlTitle = urlTitle.Substring(0, urlTitle.Length - 1);

                    return urlTitle;
                }

                return "";
            } catch (Exception e)
            {
                return "";
            }            
        }
    }
}